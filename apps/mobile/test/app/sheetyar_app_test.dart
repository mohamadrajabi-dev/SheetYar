import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:flutter_test/flutter_test.dart';
import 'package:sheetyar_mobile/app/sheetyar_app.dart';

void main() {
  testWidgets('uses the English Material 3 application shell', (
    WidgetTester tester,
  ) async {
    await tester.pumpWidget(const ProviderScope(child: SheetYarApp()));
    await tester.pumpAndSettle();

    expect(find.text('SheetYar'), findsOneWidget);
    expect(find.text('Create spreadsheets with confidence.'), findsOneWidget);

    final MaterialApp app = tester.widget<MaterialApp>(
      find.byType(MaterialApp),
    );
    expect(app.locale, const Locale('en', 'US'));
    expect(app.theme?.useMaterial3, isTrue);
    expect(app.routerConfig, isNotNull);

    final BuildContext textContext = tester.element(
      find.text('Create spreadsheets with confidence.'),
    );
    expect(Directionality.of(textContext), TextDirection.ltr);
  });
}
