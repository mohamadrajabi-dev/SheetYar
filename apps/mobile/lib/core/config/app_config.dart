import 'package:flutter/foundation.dart';

enum AppEnvironment {
  development,
  production;

  static AppEnvironment parse(String value) {
    return switch (value.trim().toLowerCase()) {
      'development' => AppEnvironment.development,
      'production' => AppEnvironment.production,
      _ => throw ArgumentError.value(
        value,
        'value',
        'APP_ENVIRONMENT must be Development or Production.',
      ),
    };
  }

  String get displayName => switch (this) {
    AppEnvironment.development => 'Development',
    AppEnvironment.production => 'Production',
  };
}

final class AppConfig {
  const AppConfig._({required this.environment, required this.apiBaseUri});

  static const String developmentAndroidEmulatorApiBaseUrl =
      'http://10.0.2.2:5028';

  final AppEnvironment environment;
  final Uri apiBaseUri;

  bool get isProduction => environment == AppEnvironment.production;

  factory AppConfig.fromEnvironment() {
    const String environmentName = String.fromEnvironment(
      'APP_ENVIRONMENT',
      defaultValue: 'Development',
    );
    const String apiBaseUrl = String.fromEnvironment('API_BASE_URL');

    return AppConfig.fromValues(
      environmentName: environmentName,
      apiBaseUrl: apiBaseUrl,
      isDebugMode: kDebugMode,
    );
  }

  factory AppConfig.fromValues({
    required String environmentName,
    required String apiBaseUrl,
    required bool isDebugMode,
  }) {
    final AppEnvironment environment = AppEnvironment.parse(environmentName);
    final String trimmedApiBaseUrl = apiBaseUrl.trim();

    if (trimmedApiBaseUrl.isEmpty && environment == AppEnvironment.production) {
      throw StateError('API_BASE_URL is required in Production.');
    }

    final String resolvedApiBaseUrl = trimmedApiBaseUrl.isEmpty
        ? developmentAndroidEmulatorApiBaseUrl
        : trimmedApiBaseUrl;

    late final Uri uri;
    try {
      uri = Uri.parse(resolvedApiBaseUrl);
    } on FormatException {
      throw ArgumentError.value(
        apiBaseUrl,
        'apiBaseUrl',
        'API_BASE_URL must be a valid absolute URI.',
      );
    }

    final String scheme = uri.scheme.toLowerCase();
    if (!uri.isAbsolute ||
        uri.host.isEmpty ||
        (scheme != 'http' && scheme != 'https')) {
      throw ArgumentError.value(
        apiBaseUrl,
        'apiBaseUrl',
        'API_BASE_URL must be an absolute HTTP or HTTPS URI.',
      );
    }

    if (uri.userInfo.isNotEmpty || uri.hasQuery || uri.hasFragment) {
      throw ArgumentError.value(
        apiBaseUrl,
        'apiBaseUrl',
        'API_BASE_URL must not contain credentials, a query, or a fragment.',
      );
    }

    if (scheme == 'http' &&
        (!isDebugMode || environment != AppEnvironment.development)) {
      throw StateError(
        'Plain HTTP is allowed only in a Development debug build.',
      );
    }

    final String normalizedPath = uri.path == '/'
        ? ''
        : uri.path.replaceFirst(RegExp(r'/+$'), '');
    final Uri normalizedUri = uri.replace(
      scheme: scheme,
      host: uri.host.toLowerCase(),
      path: normalizedPath,
    );

    return AppConfig._(environment: environment, apiBaseUri: normalizedUri);
  }
}
