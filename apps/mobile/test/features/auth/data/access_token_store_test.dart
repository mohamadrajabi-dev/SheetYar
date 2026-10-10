import 'package:flutter_test/flutter_test.dart';
import 'package:sheetyar_mobile/features/auth/data/access_token_store.dart';

void main() {
  test('keeps the access token only in the in-memory store', () {
    final InMemoryAccessTokenStore store = InMemoryAccessTokenStore();

    expect(store.accessToken, isNull);

    store.write('access-token');
    expect(store.accessToken, 'access-token');

    store.clear();
    expect(store.accessToken, isNull);
  });

  test('rejects an empty access token', () {
    final InMemoryAccessTokenStore store = InMemoryAccessTokenStore();

    expect(() => store.write('   '), throwsArgumentError);
  });
}
