import 'package:flutter/material.dart';
import 'package:go_router/go_router.dart';

import '../features/home/presentation/sheetyar_home_page.dart';

GoRouter createAppRouter() {
  return GoRouter(
    initialLocation: '/',
    routes: <RouteBase>[
      GoRoute(
        path: '/',
        builder: (BuildContext context, GoRouterState state) {
          return const SheetYarHomePage();
        },
      ),
    ],
    errorBuilder: (BuildContext context, GoRouterState state) {
      return const Scaffold(body: Center(child: Text('Page not found.')));
    },
  );
}
