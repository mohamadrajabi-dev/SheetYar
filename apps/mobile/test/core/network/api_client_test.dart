import 'dart:typed_data';

import 'package:dio/dio.dart';
import 'package:flutter_test/flutter_test.dart';
import 'package:sheetyar_mobile/core/config/app_config.dart';
import 'package:sheetyar_mobile/core/network/api_client.dart';
import 'package:sheetyar_mobile/features/auth/data/access_token_store.dart';

void main() {
  late InMemoryAccessTokenStore accessTokenStore;
  late _RecordingHttpClientAdapter adapter;
  late Dio dio;

  setUp(() {
    accessTokenStore = InMemoryAccessTokenStore()..write('access-token');
    adapter = _RecordingHttpClientAdapter();
    dio = createApiClient(
      appConfig: AppConfig.fromValues(
        environmentName: 'Development',
        apiBaseUrl: 'http://10.0.2.2:5028',
        isDebugMode: true,
      ),
      accessTokenStore: accessTokenStore,
    )..httpClientAdapter = adapter;
  });

  tearDown(() {
    dio.close(force: true);
  });

  test('adds the memory access token to same-origin API requests', () async {
    await dio.get<void>('/health');

    final RequestOptions request = adapter.requests.single;
    expect(request.headers['authorization'], 'Bearer access-token');
    expect(request.followRedirects, isFalse);
  });

  test('removes authorization from cross-origin requests', () async {
    await dio.get<void>(
      'https://external.example/health',
      options: Options(
        headers: <String, Object>{
          'Authorization': 'Bearer caller-supplied-token',
        },
      ),
    );

    final RequestOptions request = adapter.requests.single;
    expect(
      request.headers.keys.any(
        (String name) => name.toLowerCase() == 'authorization',
      ),
      isFalse,
    );
  });
}

final class _RecordingHttpClientAdapter implements HttpClientAdapter {
  final List<RequestOptions> requests = <RequestOptions>[];

  @override
  Future<ResponseBody> fetch(
    RequestOptions options,
    Stream<Uint8List>? requestStream,
    Future<void>? cancelFuture,
  ) async {
    requests.add(options);
    return ResponseBody.fromString(
      '{}',
      200,
      headers: <String, List<String>>{
        Headers.contentTypeHeader: <String>[Headers.jsonContentType],
      },
    );
  }

  @override
  void close({bool force = false}) {}
}
