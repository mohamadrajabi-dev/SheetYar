import 'package:flutter_test/flutter_test.dart';
import 'package:sheetyar_mobile/core/config/app_config.dart';

void main() {
  group('AppConfig', () {
    test('uses the Android Emulator host for Development by default', () {
      final AppConfig config = AppConfig.fromValues(
        environmentName: 'Development',
        apiBaseUrl: '',
        isDebugMode: true,
      );

      expect(config.environment, AppEnvironment.development);
      expect(
        config.apiBaseUri,
        Uri.parse(AppConfig.developmentAndroidEmulatorApiBaseUrl),
      );
    });

    test('accepts an HTTPS Production API', () {
      final AppConfig config = AppConfig.fromValues(
        environmentName: 'Production',
        apiBaseUrl: 'https://api.example.com/',
        isDebugMode: false,
      );

      expect(config.isProduction, isTrue);
      expect(config.apiBaseUri, Uri.parse('https://api.example.com'));
    });

    test('rejects plain HTTP outside a debug build', () {
      expect(
        () => AppConfig.fromValues(
          environmentName: 'Development',
          apiBaseUrl: 'http://10.0.2.2:5028',
          isDebugMode: false,
        ),
        throwsStateError,
      );
    });

    test('rejects plain HTTP in Production even when debugging', () {
      expect(
        () => AppConfig.fromValues(
          environmentName: 'Production',
          apiBaseUrl: 'http://api.example.com',
          isDebugMode: true,
        ),
        throwsStateError,
      );
    });

    test('rejects credentials, queries, and fragments in the API URL', () {
      for (final String invalidUrl in <String>[
        'https://user:password@api.example.com',
        'https://api.example.com?token=value',
        'https://api.example.com#fragment',
      ]) {
        expect(
          () => AppConfig.fromValues(
            environmentName: 'Production',
            apiBaseUrl: invalidUrl,
            isDebugMode: false,
          ),
          throwsArgumentError,
        );
      }
    });

    test('rejects an unknown environment', () {
      expect(
        () => AppConfig.fromValues(
          environmentName: 'Staging',
          apiBaseUrl: 'https://api.example.com',
          isDebugMode: false,
        ),
        throwsArgumentError,
      );
    });
  });
}
