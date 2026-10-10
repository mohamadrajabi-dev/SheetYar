# SheetYar Mobile

Flutter application for Android and iOS. All user-facing content is English, left-to-right, and formatted for `en-US`.

## Runtime Configuration

Runtime values are supplied with `--dart-define`:

- `APP_ENVIRONMENT`: `Development` or `Production`.
- `API_BASE_URL`: absolute API origin, optionally followed by a base path.

Development defaults to `http://10.0.2.2:5028`, which reaches the host machine from the Android Emulator. Plain HTTP is accepted only by a Development debug build. The Android debug network policy permits cleartext only for `10.0.2.2`; profile and release builds deny all cleartext traffic.

Run the Android Emulator development build:

```powershell
flutter run -d emulator-5554 --dart-define=APP_ENVIRONMENT=Development --dart-define=API_BASE_URL=http://10.0.2.2:5028
```

Build a production Android artifact with an HTTPS API:

```powershell
flutter build apk --release --dart-define=APP_ENVIRONMENT=Production --dart-define=API_BASE_URL=https://api.example.com
```

iOS development and release builds require macOS and Xcode. No iOS App Transport Security exception is configured, so use HTTPS on iOS.

`dart-define` values are compiled into the application. Never place secrets, credentials, signing keys, access tokens, or refresh tokens in them.

Run `flutter analyze` and `flutter test` before submitting changes.
