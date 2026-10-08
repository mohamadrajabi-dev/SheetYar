# Development Environment Audit

- **Audit date:** 2026-10-07
- **Host:** Windows 10 Pro 64-bit, version 22H2, build 19045
- **Scope:** Read-only inspection. No software, SDK package, license, service, environment variable, or configuration was installed or changed.

## Build Policy

This Windows workstation is supported for SheetYar Android builds only. The Flutter source may contain an iOS target, but iOS compilation, Simulator testing, code signing, archiving, and App Store release require a Mac running macOS with Xcode. They cannot be performed on this Windows host.

## Readiness Summary

| Area | Status | Result |
|---|---|---|
| Flutter SDK | Ready | Flutter 3.47.5 stable at `E:\Dev\flutter` |
| Dart SDK | Ready | Dart 3.13.4 bundled with Flutter |
| Java | Ready | Android builds use Android Studio JBR 25.0.3 |
| Android Studio | Ready | 2026.1.4 installed with Flutter and Dart plugins |
| Android SDK base tools | Ready | SDK detected at `D:\Android\SDK`; licenses accepted |
| Android SDK Platform 36 | **Missing** | Flutter 3.47.5 defaults to `compileSdk 36`, but only Platform 37 is installed |
| Android NDK 28.2 | **Missing** | Flutter defaults to 28.2.13676358; only 27.1.12297006 is installed |
| Android Emulator | Ready | Emulator 37.1.11; WHPX acceleration operational |
| Android System Image | Ready | Android 16 / API 36, Google APIs, x86_64, revision 7 |
| Android AVD | Ready, stopped | `SheetYar_Pixel_8_API_36` exists but was not running during the audit |
| .NET SDK | Ready | SDK 8.0.302 and 10.0.100 installed |
| SQL Server | Ready for local development | SQL Server 2022 Developer is installed for development and testing |
| Git | Ready | Git 2.47.1 with Credential Manager and the SheetYar remote configured |
| iOS toolchain | Not available by design | Requires macOS and Xcode |

## Flutter Doctor

`flutter doctor -v` completed successfully for Flutter and the Android toolchain:

- Flutter 3.47.5 stable, Dart 3.13.4, and DevTools 2.60.0 were detected.
- Android SDK `D:\Android\SDK` was detected and all Android licenses were accepted.
- No Android device was connected because the AVD was stopped; this is not an installation failure.
- The Visual Studio warning concerns Windows desktop C++ development. It is irrelevant because SheetYar must not build a Windows desktop target.
- Windows can build the Android target. The iOS target cannot be built or tested locally on Windows.

## Flutter, Dart, and Java

| Component | Detected Version | Detected Location | Assessment |
|---|---|---|---|
| Flutter | 3.47.5 stable | `E:\Dev\flutter` | Ready; path contains no spaces |
| Dart | 3.13.4 | `E:\Dev\flutter\bin\cache\dart-sdk` | Ready; do not install a separate Dart SDK |
| DevTools | 2.60.0 | Managed by Flutter | Ready |
| Android build Java | JBR 25.0.3 | `C:\Program Files\Android\Android Studio\jbr` | Ready; selected by Flutter doctor |
| Global Java | Microsoft JDK 17.0.20.1 | Global `JAVA_HOME` | Present, but not the Java selected for Android builds |

## Android Toolchain

### Android Studio

- **Version:** 2026.1.4, build `AI-261.26222.65.2614.16204760`
- **Location:** `C:\Program Files\Android\Android Studio`
- **Installed user plugins:** Dart and `flutter-intellij-96`
- **Status:** Ready

### Android SDK Packages

| Package | Detected Version | Location | Status |
|---|---|---|---|
| Platform-Tools | 37.0.1 | `D:\Android\SDK\platform-tools` | Ready |
| Build-Tools | 37.0.0 | `D:\Android\SDK\build-tools\37.0.0` | Ready |
| Android SDK Platform | 37.0 | `D:\Android\SDK\platforms\android-37` | Installed |
| Android SDK Platform | 36 | Expected at `D:\Android\SDK\platforms\android-36` | **Missing; required by the current Flutter default** |
| Emulator | 37.1.11, build 15917651 | `D:\Android\SDK\emulator` | Ready |
| System Image | API 36 Google APIs x86_64, revision 7 | `D:\Android\SDK\system-images\android-36\google_apis\x86_64` | Ready |
| CMake | 3.30.5 | `D:\Android\SDK\cmake\3.30.5` | Ready |
| Android NDK | 27.1.12297006 | `D:\Android\SDK\ndk\27.1.12297006` | Installed but not Flutter's default |
| Android NDK | 28.2.13676358 | Expected at `D:\Android\SDK\ndk\28.2.13676358` | **Missing; install before deterministic native-plugin builds** |

Android Studio SDK Manager should own all SDK package installation. Do not manually copy individual emulator, platform, system-image, or NDK files into the SDK.

### Emulator, AVD, and Virtualization

- **AVD:** `SheetYar_Pixel_8_API_36`
- **AVD location:** `C:\Users\PC\.android\avd\SheetYar_Pixel_8_API_36.avd`
- **Device profile:** Pixel 8, Android 16 / API 36, Google APIs, x86_64
- **State during audit:** Stopped; `adb devices` returned no Android device
- **Acceleration:** `emulator -accel-check` reported WHPX 10.0.19045 installed and usable
- **Hypervisor:** Present and operational
- **Old SDK references:** No remaining `D:\Andruid studio` reference was found in the AVD configuration

No BIOS or Windows virtualization change is currently required. Start the existing AVD when an Android device is needed.

## .NET Toolchain

| Component | Detected | Location | Assessment |
|---|---|---|---|
| .NET SDK | 8.0.302, 10.0.100 | `C:\Program Files\dotnet` | Ready for ASP.NET Core development |
| ASP.NET Core runtimes | 8.0.6, 8.0.22, 10.0.0 | `C:\Program Files\dotnet\shared` | Ready |
| `global.json` | Not present | Repository root when created | Not a machine shortage; pin the chosen SDK during scaffolding |
| `dotnet-ef` | Not installed globally | Future `E:\SheetYar\.config\dotnet-tools.json` | Correctly deferred; install later as a repository-local tool |

## SQL Server

- Two automatic, running SQL Server 2022 Developer instances were detected:
  - Default instance `MSSQLSERVER`, version 16.0.1000.6
  - Named instance `MSSQLSERVERR`, version 16.0.1000.6
- Engine locations:
  - `C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL`
  - `C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVERR\MSSQL`
- `sqlcmd` 16.0.1000.6 is installed.
- SQL Server Management Studio 20.1.10.0 is installed.
- LocalDB binaries and an `MSSQLLocalDB` instance were detected, but per-user instance status could not be validated from the sandbox account.
- A live database login was not tested because the audit runs under an isolated Windows account. This is an unverified check, not a detected SQL failure.

### SQL Findings

1. SQL Server 2022 Developer is suitable for local development and testing, but Microsoft does not license Developer edition for production use.
2. The production SQL Server edition, capacity, topology, and licensing must be selected and validated separately before deployment.
3. Both detected SQL Server engines report the original 16.0.1000.6 build. Review and apply the current SQL Server 2022 cumulative update before regular use; no update was applied during this audit.
4. The additional `MSSQLSERVERR` instance may be intentional and was not changed or removed.

## Git

- **Version:** 2.47.1.windows.1
- **Location:** `C:\Program Files\Git\cmd\git.exe`
- **Credential Manager:** 2.6.0, helper `manager`
- **Identity:** User name and email configured
- **Default branch:** `main`
- **SheetYar remote:** `https://github.com/mohamadrajabi-dev/SheetYar.git`
- **Status:** Ready

The Codex sandbox may report Git dubious ownership because it uses a different Windows account. This is not a repository or user Git failure, and the user's global `safe.directory` configuration should not be changed for it.

## Required and Deferred Actions

### Required Before the First Android Build

1. In Android Studio SDK Manager, install **Android SDK Platform 36** into `D:\Android\SDK\platforms\android-36`.
2. In SDK Manager > SDK Tools, install **NDK (Side by side) 28.2.13676358** into `D:\Android\SDK\ndk\28.2.13676358`.
3. Start `SheetYar_Pixel_8_API_36` and confirm it appears in `flutter devices`.

### Project Decisions or Later Setup

1. Select and license the production SQL Server edition based on deployment capacity and availability requirements; keep Developer edition limited to development and testing.
2. Review SQL Server servicing against the current official SQL Server 2022 build list.
3. Pin the selected .NET SDK with `global.json` when the API is scaffolded.
4. Add `dotnet-ef` as a repository-local tool only when EF Core work begins.
5. Validate the eventual API connection string with a live, non-destructive SQL connection test.

## Official Downloads and Suggested Locations

| Component | Official Source | Suggested Location | Current Need |
|---|---|---|---|
| Flutter SDK | [Flutter manual installation](https://docs.flutter.dev/install/manual) | Keep `E:\Dev\flutter` | Already installed |
| Dart SDK | [Get the Dart SDK](https://dart.dev/get-dart) | Bundled under Flutter | Do not install separately |
| Android Studio | [Android Studio](https://developer.android.com/studio) | `C:\Program Files\Android\Android Studio` | Already installed |
| Android SDK packages | [SDK Manager](https://developer.android.com/studio/intro/update#sdk-manager) | Keep `D:\Android\SDK` | Install Platform 36 through SDK Manager |
| Android NDK | [Install and configure the NDK](https://developer.android.com/studio/projects/install-ndk) | `D:\Android\SDK\ndk\28.2.13676358` | Install version 28.2.13676358 through SDK Manager |
| Android Emulator and AVD | [Flutter Android setup](https://docs.flutter.dev/platform-integration/android/setup?tab=virtual), [Manage AVDs](https://developer.android.com/studio/run/managing-avds) | Emulator under SDK; AVD under `C:\Users\PC\.android\avd` | Already installed |
| Windows virtualization | [Android emulator acceleration](https://developer.android.com/studio/run/emulator-acceleration), [Microsoft virtualization guidance](https://support.microsoft.com/en-us/windows/experience/enable-virtualization-on-windows) | Windows feature and BIOS/UEFI | Already operational |
| Java | [Android build JDK guidance](https://developer.android.com/build/jdks) | Use Android Studio `jbr` | No separate install needed |
| .NET SDK | [.NET Windows downloads](https://dotnet.microsoft.com/en-us/download?initial-os=windows), [.NET 10](https://dotnet.microsoft.com/en-us/download/dotnet/10.0) | `C:\Program Files\dotnet` | Already installed |
| SQL Server | [SQL Server downloads](https://www.microsoft.com/en-us/sql-server/sql-server-downloads), [SQL Server 2022 editions and supported features](https://learn.microsoft.com/en-us/sql/sql-server/editions-and-components-of-sql-server-2022) | `C:\Program Files\Microsoft SQL Server` | Developer edition is installed for local development and testing; choose production edition separately |
| SQL Server servicing | [SQL Server 2022 build versions](https://learn.microsoft.com/en-us/troubleshoot/sql/releases/sqlserver-2022/build-versions) | Existing instances | Review current cumulative update |
| Git for Windows | [Git for Windows](https://git-scm.com/install/windows) | `C:\Program Files\Git` | Already installed |
| Xcode for iOS | [Flutter iOS setup](https://docs.flutter.dev/platform-integration/ios/setup), [Xcode](https://developer.apple.com/xcode/) | `/Applications/Xcode.app` on a Mac | Required only on macOS; impossible to install on Windows |

## Read-Only Verification Commands Used

- `flutter doctor -v`, `flutter --version`, `dart --version`
- Java and Android Studio version inspection
- Android SDK package metadata inspection
- `emulator -version`, `emulator -list-avds`, and `emulator -accel-check`
- `adb devices`
- `dotnet --info` and `dotnet --list-sdks`
- SQL Server service, registry, executable, and edition inspection
- `git --version`, Git configuration, remote, and branch inspection

No install, update, license acceptance, emulator start, service change, environment-variable change, commit, or push was performed.
