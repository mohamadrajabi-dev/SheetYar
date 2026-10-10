import 'package:flutter/material.dart';

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
