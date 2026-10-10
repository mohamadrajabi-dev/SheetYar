/// Persists only the refresh token. Access tokens must remain in memory.
abstract interface class RefreshTokenStore {
  Future<String?> read();

  Future<void> write(String refreshToken);

  Future<void> clear();
}

abstract interface class SecureKeyValueStore {
  Future<String?> read({required String key});

  Future<void> write({required String key, required String value});

  Future<void> delete({required String key});
}

final class SecureRefreshTokenStore implements RefreshTokenStore {
  SecureRefreshTokenStore(this._storage);

  static const String storageKey = 'sheetyar.auth.refresh_token.v1';

  final SecureKeyValueStore _storage;

  @override
  Future<String?> read() => _storage.read(key: storageKey);

  @override
  Future<void> write(String refreshToken) {
    if (refreshToken.isEmpty) {
      throw ArgumentError.value(
        refreshToken,
        'refreshToken',
        'Refresh token must not be empty.',
      );
    }

    return _storage.write(key: storageKey, value: refreshToken);
  }

  @override
  Future<void> clear() => _storage.delete(key: storageKey);
}
