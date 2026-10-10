import 'package:flutter_secure_storage/flutter_secure_storage.dart';

import 'refresh_token_store.dart';

const String _androidStorageNamespace = 'sheetyar.auth';

final class FlutterSecureStorageAdapter implements SecureKeyValueStore {
  FlutterSecureStorageAdapter({FlutterSecureStorage? storage})
    : _storage =
          storage ??
          const FlutterSecureStorage(
            aOptions: androidOptions,
            iOptions: iosOptions,
          );

  static const String androidStorageNamespace = _androidStorageNamespace;
  static const AndroidOptions androidOptions = AndroidOptions(
    storageNamespace: _androidStorageNamespace,
  );
  static const IOSOptions iosOptions = IOSOptions(
    accessibility: KeychainAccessibility.first_unlock_this_device,
    synchronizable: false,
  );

  final FlutterSecureStorage _storage;

  @override
  Future<void> delete({required String key}) => _storage.delete(key: key);

  @override
  Future<String?> read({required String key}) => _storage.read(key: key);

  @override
  Future<void> write({required String key, required String value}) =>
      _storage.write(key: key, value: value);
}
