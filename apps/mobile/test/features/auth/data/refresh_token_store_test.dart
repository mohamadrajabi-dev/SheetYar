import 'package:flutter_secure_storage/flutter_secure_storage.dart';
import 'package:flutter_test/flutter_test.dart';
import 'package:sheetyar_mobile/features/auth/data/flutter_secure_storage_adapter.dart';
import 'package:sheetyar_mobile/features/auth/data/refresh_token_store.dart';

void main() {
  group('SecureRefreshTokenStore', () {
    test('writes and reads only the versioned refresh-token key', () async {
      final _FakeSecureKeyValueStore storage = _FakeSecureKeyValueStore();
      final SecureRefreshTokenStore store = SecureRefreshTokenStore(storage);

      await store.write('refresh-token');

      expect(await store.read(), 'refresh-token');
      expect(storage.values, <String, String>{
        SecureRefreshTokenStore.storageKey: 'refresh-token',
      });
    });

    test(
      'clears the refresh token without removing other secure values',
      () async {
        final _FakeSecureKeyValueStore storage = _FakeSecureKeyValueStore()
          ..values.addAll(<String, String>{
            SecureRefreshTokenStore.storageKey: 'refresh-token',
            'unrelated': 'preserved',
          });
        final SecureRefreshTokenStore store = SecureRefreshTokenStore(storage);

        await store.clear();

        expect(await store.read(), isNull);
        expect(storage.values, <String, String>{'unrelated': 'preserved'});
      },
    );

    test('rejects an empty refresh token', () {
      final SecureRefreshTokenStore store = SecureRefreshTokenStore(
        _FakeSecureKeyValueStore(),
      );

      expect(() => store.write(''), throwsArgumentError);
    });
  });

  test('uses isolated device-bound platform storage options', () {
    expect(
      FlutterSecureStorageAdapter.androidOptions.storageNamespace,
      FlutterSecureStorageAdapter.androidStorageNamespace,
    );
    expect(
      FlutterSecureStorageAdapter.iosOptions.accessibility,
      KeychainAccessibility.first_unlock_this_device,
    );
    expect(FlutterSecureStorageAdapter.iosOptions.synchronizable, isFalse);
  });
}

final class _FakeSecureKeyValueStore implements SecureKeyValueStore {
  final Map<String, String> values = <String, String>{};

  @override
  Future<void> delete({required String key}) async {
    values.remove(key);
  }

  @override
  Future<String?> read({required String key}) async => values[key];

  @override
  Future<void> write({required String key, required String value}) async {
    values[key] = value;
  }
}
