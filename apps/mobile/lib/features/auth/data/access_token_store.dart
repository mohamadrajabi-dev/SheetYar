abstract interface class AccessTokenStore {
  String? get accessToken;

  void write(String accessToken);

  void clear();
}

final class InMemoryAccessTokenStore implements AccessTokenStore {
  String? _accessToken;

  @override
  String? get accessToken => _accessToken;

  @override
  void write(String accessToken) {
    final String normalizedToken = accessToken.trim();
    if (normalizedToken.isEmpty) {
      throw ArgumentError.value(
        accessToken,
        'accessToken',
        'Access token must not be empty.',
      );
    }

    _accessToken = normalizedToken;
  }

  @override
  void clear() => _accessToken = null;
}
