import 'package:flutter/material.dart';

class SheetYarApp extends StatelessWidget {
  const SheetYarApp({super.key});

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'SheetYar',
      debugShowCheckedModeBanner: false,
      locale: const Locale('en', 'US'),
      supportedLocales: const <Locale>[Locale('en', 'US')],
      theme: ThemeData(
        colorScheme: ColorScheme.fromSeed(seedColor: Colors.green),
        useMaterial3: true,
      ),
      builder: (BuildContext context, Widget? child) {
        return Directionality(
          textDirection: TextDirection.ltr,
          child: child ?? const SizedBox.shrink(),
        );
      },
      home: const SheetYarHomePage(),
    );
  }
}

class SheetYarHomePage extends StatelessWidget {
  const SheetYarHomePage({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: const Text('SheetYar')),
      body: const Center(child: Text('Create spreadsheets with confidence.')),
    );
  }
}
