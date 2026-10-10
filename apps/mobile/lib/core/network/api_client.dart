import 'package:dio/dio.dart';

import '../../features/auth/data/access_token_store.dart';
import '../config/app_config.dart';

Dio createApiClient({
  required AppConfig appConfig,
  required AccessTokenStore accessTokenStore,
}) {
  final Dio dio = Dio(
    BaseOptions(
      baseUrl: appConfig.apiBaseUri.toString(),
      connectTimeout: const Duration(seconds: 15),
      sendTimeout: const Duration(seconds: 30),
      receiveTimeout: const Duration(seconds: 30),
      followRedirects: false,
      headers: <String, Object>{Headers.acceptHeader: Headers.jsonContentType},
    ),
  );

  dio.interceptors.add(
    AccessTokenInterceptor(
      apiBaseUri: appConfig.apiBaseUri,
      accessTokenStore: accessTokenStore,
    ),
  );

  return dio;
}

final class AccessTokenInterceptor extends Interceptor {
  AccessTokenInterceptor({
    required Uri apiBaseUri,
    required AccessTokenStore accessTokenStore,
  }) : _apiBaseUri = apiBaseUri,
       _accessTokenStore = accessTokenStore;

  final Uri _apiBaseUri;
  final AccessTokenStore _accessTokenStore;

  @override
  void onRequest(RequestOptions options, RequestInterceptorHandler handler) {
    options.headers.removeWhere(
      (String name, Object? value) => name.toLowerCase() == 'authorization',
    );

    final String? accessToken = _accessTokenStore.accessToken;
    if (accessToken != null && _hasSameOrigin(options.uri, _apiBaseUri)) {
      options.headers['authorization'] = 'Bearer $accessToken';
    }

    handler.next(options);
  }
}

bool _hasSameOrigin(Uri first, Uri second) {
  return first.scheme.toLowerCase() == second.scheme.toLowerCase() &&
      first.host.toLowerCase() == second.host.toLowerCase() &&
      _effectivePort(first) == _effectivePort(second);
}

int _effectivePort(Uri uri) {
  if (uri.hasPort) {
    return uri.port;
  }

  return switch (uri.scheme.toLowerCase()) {
    'http' => 80,
    'https' => 443,
    _ => uri.port,
  };
}
