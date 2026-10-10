import 'package:dio/dio.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:go_router/go_router.dart';

import '../core/config/app_config.dart';
import '../core/network/api_client.dart';
import '../features/auth/data/access_token_store.dart';
import '../features/auth/data/flutter_secure_storage_adapter.dart';
import '../features/auth/data/refresh_token_store.dart';
import 'app_router.dart';

final Provider<AppConfig> appConfigProvider = Provider<AppConfig>(
  (Ref ref) => AppConfig.fromEnvironment(),
);

final Provider<AccessTokenStore> accessTokenStoreProvider =
    Provider<AccessTokenStore>((Ref ref) => InMemoryAccessTokenStore());

final Provider<RefreshTokenStore> refreshTokenStoreProvider =
    Provider<RefreshTokenStore>(
      (Ref ref) => SecureRefreshTokenStore(FlutterSecureStorageAdapter()),
    );

final Provider<Dio> dioProvider = Provider<Dio>((Ref ref) {
  final Dio dio = createApiClient(
    appConfig: ref.watch(appConfigProvider),
    accessTokenStore: ref.watch(accessTokenStoreProvider),
  );
  ref.onDispose(dio.close);
  return dio;
});

final Provider<GoRouter> appRouterProvider = Provider<GoRouter>((Ref ref) {
  final GoRouter router = createAppRouter();
  ref.onDispose(router.dispose);
  return router;
});
