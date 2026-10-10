# Dependency License Register

All direct and transitive third-party dependencies must be registered before use.

## Policy

- A dependency may be added only if its license is MIT, BSD-2-Clause, BSD-3-Clause, or Apache-2.0.
- Verify the license using an authoritative package registry or the dependency's official source repository.
- Complete the register below before adding or updating the dependency in project files.
- Record the exact resolved version; do not use an unverified floating version.
- Do not use commercial, subscription-based, source-available, dual-licensed-with-fees, or otherwise restricted components.
- The only approved exception is `Microsoft.Data.SqlClient.SNI.runtime` at the exact registered version. It is an unavoidable transitive dependency of the official EF Core SQL Server provider and was explicitly approved on 2026-10-08.

## Prohibited Dependencies and Services

- Syncfusion
- DevExpress
- Telerik
- Aspose
- Infragistics
- SpreadJS
- `@univerjs-pro`
- Paid cloud services
- Subscription-required dependencies or services

## Register

| Dependency | Ecosystem | Exact Version | License | Authoritative Source | Purpose | Verified Date |
|---|---|---:|---|---|---|---|
| Flutter SDK (`flutter`, `flutter_test`) | Flutter SDK | 3.47.5 | BSD-3-Clause | https://github.com/flutter/flutter/blob/3.47.5/LICENSE | Mobile UI framework and Flutter testing support | 2026-10-07 |
| Microsoft.NET.Test.Sdk | NuGet | 17.8.0 | MIT | https://www.nuget.org/packages/Microsoft.NET.Test.Sdk/17.8.0/License | .NET test build and execution support | 2026-10-07 |
| xunit | NuGet | 2.5.3 | Apache-2.0 | https://www.nuget.org/packages/xunit/2.5.3 | Backend unit testing framework | 2026-10-07 |
| xunit.runner.visualstudio | NuGet | 2.5.3 | Apache-2.0 | https://www.nuget.org/packages/xunit.runner.visualstudio/2.5.3 | xUnit adapter for `dotnet test` and IDE runners | 2026-10-07 |
| Swashbuckle.AspNetCore.Swagger | NuGet | 6.6.2 | MIT | https://www.nuget.org/packages/Swashbuckle.AspNetCore.Swagger/6.6.2 | Serve generated OpenAPI documents | 2026-10-07 |
| Swashbuckle.AspNetCore.SwaggerGen | NuGet | 6.6.2 | MIT | https://www.nuget.org/packages/Swashbuckle.AspNetCore.SwaggerGen/6.6.2 | Generate OpenAPI documents from API endpoints | 2026-10-07 |
| Microsoft.OpenApi | NuGet | 1.6.14 | MIT | https://www.nuget.org/packages/Microsoft.OpenApi/1.6.14 | OpenAPI object model used by Swagger generation | 2026-10-07 |
| Azure.Core | NuGet | 1.41.0 | MIT | https://www.nuget.org/packages/Azure.Core/1.41.0 | Transitive dependency of EF Core SQL Server or EF Core design tooling | 2026-10-08 |
| Azure.Identity | NuGet | 1.12.1 | MIT | https://www.nuget.org/packages/Azure.Identity/1.12.1 | Transitive dependency of EF Core SQL Server or EF Core design tooling | 2026-10-08 |
| Humanizer.Core | NuGet | 2.14.1 | MIT | https://www.nuget.org/packages/Humanizer.Core/2.14.1 | Transitive dependency of EF Core SQL Server or EF Core design tooling | 2026-10-08 |
| Microsoft.Bcl.AsyncInterfaces | NuGet | 6.0.0 | MIT | https://www.nuget.org/packages/Microsoft.Bcl.AsyncInterfaces/6.0.0 | Transitive dependency of EF Core SQL Server or EF Core design tooling | 2026-10-08 |
| Microsoft.CodeAnalysis.Analyzers | NuGet | 3.3.3 | MIT | https://www.nuget.org/packages/Microsoft.CodeAnalysis.Analyzers/3.3.3 | Transitive dependency of EF Core SQL Server or EF Core design tooling | 2026-10-08 |
| Microsoft.CodeAnalysis.Common | NuGet | 4.5.0 | MIT | https://www.nuget.org/packages/Microsoft.CodeAnalysis.Common/4.5.0 | Transitive dependency of EF Core SQL Server or EF Core design tooling | 2026-10-08 |
| Microsoft.CodeAnalysis.CSharp.Workspaces | NuGet | 4.5.0 | MIT | https://www.nuget.org/packages/Microsoft.CodeAnalysis.CSharp.Workspaces/4.5.0 | Transitive dependency of EF Core SQL Server or EF Core design tooling | 2026-10-08 |
| Microsoft.CodeAnalysis.CSharp | NuGet | 4.5.0 | MIT | https://www.nuget.org/packages/Microsoft.CodeAnalysis.CSharp/4.5.0 | Transitive dependency of EF Core SQL Server or EF Core design tooling | 2026-10-08 |
| Microsoft.CodeAnalysis.Workspaces.Common | NuGet | 4.5.0 | MIT | https://www.nuget.org/packages/Microsoft.CodeAnalysis.Workspaces.Common/4.5.0 | Transitive dependency of EF Core SQL Server or EF Core design tooling | 2026-10-08 |
| Microsoft.CSharp | NuGet | 4.5.0 | MIT | https://www.nuget.org/packages/Microsoft.CSharp/4.5.0 | Transitive dependency of EF Core SQL Server or EF Core design tooling | 2026-10-08 |
| Microsoft.Data.SqlClient.SNI.runtime | NuGet | 5.1.2 | Microsoft SQL Client SNI Redistributable License (approved exception) | https://www.nuget.org/packages/Microsoft.Data.SqlClient.SNI.runtime/5.1.2 | Native SQL Server connectivity required transitively by Microsoft.Data.SqlClient | 2026-10-08 |
| Microsoft.Data.SqlClient | NuGet | 5.1.9 | MIT | https://www.nuget.org/packages/Microsoft.Data.SqlClient/5.1.9 | Transitive dependency of EF Core SQL Server or EF Core design tooling | 2026-10-08 |
| Microsoft.EntityFrameworkCore.Abstractions | NuGet | 8.0.31 | MIT | https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.Abstractions/8.0.31 | Transitive dependency of EF Core SQL Server or EF Core design tooling | 2026-10-08 |
| Microsoft.EntityFrameworkCore.Analyzers | NuGet | 8.0.31 | MIT | https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.Analyzers/8.0.31 | Transitive dependency of EF Core SQL Server or EF Core design tooling | 2026-10-08 |
| Microsoft.EntityFrameworkCore.Design | NuGet | 8.0.31 | MIT | https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.Design/8.0.31 | EF Core design-time migration support | 2026-10-08 |
| Microsoft.EntityFrameworkCore.Relational | NuGet | 8.0.31 | MIT | https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.Relational/8.0.31 | Transitive dependency of EF Core SQL Server or EF Core design tooling | 2026-10-08 |
| Microsoft.EntityFrameworkCore.SqlServer | NuGet | 8.0.31 | MIT | https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.SqlServer/8.0.31 | EF Core provider for SQL Server | 2026-10-08 |
| Microsoft.EntityFrameworkCore | NuGet | 8.0.31 | MIT | https://www.nuget.org/packages/Microsoft.EntityFrameworkCore/8.0.31 | Transitive dependency of EF Core SQL Server or EF Core design tooling | 2026-10-08 |
| Microsoft.Extensions.Caching.Abstractions | NuGet | 8.0.0 | MIT | https://www.nuget.org/packages/Microsoft.Extensions.Caching.Abstractions/8.0.0 | Transitive dependency of EF Core SQL Server or EF Core design tooling | 2026-10-08 |
| Microsoft.Extensions.Caching.Memory | NuGet | 8.0.1 | MIT | https://www.nuget.org/packages/Microsoft.Extensions.Caching.Memory/8.0.1 | Transitive dependency of EF Core SQL Server or EF Core design tooling | 2026-10-08 |
| Microsoft.Extensions.Configuration.Abstractions | NuGet | 8.0.0 | MIT | https://www.nuget.org/packages/Microsoft.Extensions.Configuration.Abstractions/8.0.0 | Transitive dependency of EF Core SQL Server or EF Core design tooling | 2026-10-08 |
| Microsoft.Extensions.DependencyInjection.Abstractions | NuGet | 8.0.2 | MIT | https://www.nuget.org/packages/Microsoft.Extensions.DependencyInjection.Abstractions/8.0.2 | Transitive dependency of EF Core SQL Server or EF Core design tooling | 2026-10-08 |
| Microsoft.Extensions.DependencyInjection | NuGet | 8.0.1 | MIT | https://www.nuget.org/packages/Microsoft.Extensions.DependencyInjection/8.0.1 | Transitive dependency of EF Core SQL Server or EF Core design tooling | 2026-10-08 |
| Microsoft.Extensions.DependencyModel | NuGet | 8.0.2 | MIT | https://www.nuget.org/packages/Microsoft.Extensions.DependencyModel/8.0.2 | Transitive dependency of EF Core SQL Server or EF Core design tooling | 2026-10-08 |
| Microsoft.Extensions.Logging.Abstractions | NuGet | 8.0.2 | MIT | https://www.nuget.org/packages/Microsoft.Extensions.Logging.Abstractions/8.0.2 | Transitive dependency of EF Core SQL Server or EF Core design tooling | 2026-10-08 |
| Microsoft.Extensions.Logging | NuGet | 8.0.1 | MIT | https://www.nuget.org/packages/Microsoft.Extensions.Logging/8.0.1 | Transitive dependency of EF Core SQL Server or EF Core design tooling | 2026-10-08 |
| Microsoft.Extensions.Options | NuGet | 8.0.2 | MIT | https://www.nuget.org/packages/Microsoft.Extensions.Options/8.0.2 | Transitive dependency of EF Core SQL Server or EF Core design tooling | 2026-10-08 |
| Microsoft.Extensions.Primitives | NuGet | 8.0.0 | MIT | https://www.nuget.org/packages/Microsoft.Extensions.Primitives/8.0.0 | Transitive dependency of EF Core SQL Server or EF Core design tooling | 2026-10-08 |
| Microsoft.Identity.Client.Extensions.Msal | NuGet | 4.65.0 | MIT | https://www.nuget.org/packages/Microsoft.Identity.Client.Extensions.Msal/4.65.0 | Transitive dependency of EF Core SQL Server or EF Core design tooling | 2026-10-08 |
| Microsoft.Identity.Client | NuGet | 4.76.0 | MIT | https://www.nuget.org/packages/Microsoft.Identity.Client/4.76.0 | Transitive dependency of EF Core SQL Server or EF Core design tooling | 2026-10-08 |
| Microsoft.IdentityModel.Abstractions | NuGet | 6.35.0 | MIT | https://www.nuget.org/packages/Microsoft.IdentityModel.Abstractions/6.35.0 | Transitive dependency of EF Core SQL Server or EF Core design tooling | 2026-10-08 |
| Microsoft.IdentityModel.JsonWebTokens | NuGet | 6.35.0 | MIT | https://www.nuget.org/packages/Microsoft.IdentityModel.JsonWebTokens/6.35.0 | Transitive dependency of EF Core SQL Server or EF Core design tooling | 2026-10-08 |
| Microsoft.IdentityModel.Logging | NuGet | 6.35.0 | MIT | https://www.nuget.org/packages/Microsoft.IdentityModel.Logging/6.35.0 | Transitive dependency of EF Core SQL Server or EF Core design tooling | 2026-10-08 |
| Microsoft.IdentityModel.Protocols.OpenIdConnect | NuGet | 6.35.0 | MIT | https://www.nuget.org/packages/Microsoft.IdentityModel.Protocols.OpenIdConnect/6.35.0 | Transitive dependency of EF Core SQL Server or EF Core design tooling | 2026-10-08 |
| Microsoft.IdentityModel.Protocols | NuGet | 6.35.0 | MIT | https://www.nuget.org/packages/Microsoft.IdentityModel.Protocols/6.35.0 | Transitive dependency of EF Core SQL Server or EF Core design tooling | 2026-10-08 |
| Microsoft.IdentityModel.Tokens | NuGet | 6.35.0 | MIT | https://www.nuget.org/packages/Microsoft.IdentityModel.Tokens/6.35.0 | Transitive dependency of EF Core SQL Server or EF Core design tooling | 2026-10-08 |
| Microsoft.NETCore.Platforms | NuGet | 1.1.0 | MIT | https://www.nuget.org/packages/Microsoft.NETCore.Platforms/1.1.0 | Transitive dependency of EF Core SQL Server or EF Core design tooling | 2026-10-08 |
| Microsoft.NETCore.Targets | NuGet | 1.1.0 | MIT | https://www.nuget.org/packages/Microsoft.NETCore.Targets/1.1.0 | Transitive dependency of EF Core SQL Server or EF Core design tooling | 2026-10-08 |
| Microsoft.SqlServer.Server | NuGet | 1.0.0 | MIT | https://www.nuget.org/packages/Microsoft.SqlServer.Server/1.0.0 | Transitive dependency of EF Core SQL Server or EF Core design tooling | 2026-10-08 |
| Microsoft.Win32.SystemEvents | NuGet | 6.0.0 | MIT | https://www.nuget.org/packages/Microsoft.Win32.SystemEvents/6.0.0 | Transitive dependency of EF Core SQL Server or EF Core design tooling | 2026-10-08 |
| Mono.TextTemplating | NuGet | 2.2.1 | MIT | https://www.nuget.org/packages/Mono.TextTemplating/2.2.1 | Transitive dependency of EF Core SQL Server or EF Core design tooling | 2026-10-08 |
| System.ClientModel | NuGet | 1.0.0 | MIT | https://www.nuget.org/packages/System.ClientModel/1.0.0 | Transitive dependency of EF Core SQL Server or EF Core design tooling | 2026-10-08 |
| System.CodeDom | NuGet | 4.4.0 | MIT | https://www.nuget.org/packages/System.CodeDom/4.4.0 | Transitive dependency of EF Core SQL Server or EF Core design tooling | 2026-10-08 |
| System.Collections.Immutable | NuGet | 6.0.0 | MIT | https://www.nuget.org/packages/System.Collections.Immutable/6.0.0 | Transitive dependency of EF Core SQL Server or EF Core design tooling | 2026-10-08 |
| System.Composition.AttributedModel | NuGet | 6.0.0 | MIT | https://www.nuget.org/packages/System.Composition.AttributedModel/6.0.0 | Transitive dependency of EF Core SQL Server or EF Core design tooling | 2026-10-08 |
| System.Composition.Convention | NuGet | 6.0.0 | MIT | https://www.nuget.org/packages/System.Composition.Convention/6.0.0 | Transitive dependency of EF Core SQL Server or EF Core design tooling | 2026-10-08 |
| System.Composition.Hosting | NuGet | 6.0.0 | MIT | https://www.nuget.org/packages/System.Composition.Hosting/6.0.0 | Transitive dependency of EF Core SQL Server or EF Core design tooling | 2026-10-08 |
| System.Composition.Runtime | NuGet | 6.0.0 | MIT | https://www.nuget.org/packages/System.Composition.Runtime/6.0.0 | Transitive dependency of EF Core SQL Server or EF Core design tooling | 2026-10-08 |
| System.Composition.TypedParts | NuGet | 6.0.0 | MIT | https://www.nuget.org/packages/System.Composition.TypedParts/6.0.0 | Transitive dependency of EF Core SQL Server or EF Core design tooling | 2026-10-08 |
| System.Composition | NuGet | 6.0.0 | MIT | https://www.nuget.org/packages/System.Composition/6.0.0 | Transitive dependency of EF Core SQL Server or EF Core design tooling | 2026-10-08 |
| System.Configuration.ConfigurationManager | NuGet | 6.0.1 | MIT | https://www.nuget.org/packages/System.Configuration.ConfigurationManager/6.0.1 | Transitive dependency of EF Core SQL Server or EF Core design tooling | 2026-10-08 |
| System.Diagnostics.DiagnosticSource | NuGet | 6.0.1 | MIT | https://www.nuget.org/packages/System.Diagnostics.DiagnosticSource/6.0.1 | Transitive dependency of EF Core SQL Server or EF Core design tooling | 2026-10-08 |
| System.Drawing.Common | NuGet | 6.0.0 | MIT | https://www.nuget.org/packages/System.Drawing.Common/6.0.0 | Transitive dependency of EF Core SQL Server or EF Core design tooling | 2026-10-08 |
| System.Formats.Asn1 | NuGet | 8.0.2 | MIT | https://www.nuget.org/packages/System.Formats.Asn1/8.0.2 | Transitive dependency of EF Core SQL Server or EF Core design tooling | 2026-10-08 |
| System.IdentityModel.Tokens.Jwt | NuGet | 6.35.0 | MIT | https://www.nuget.org/packages/System.IdentityModel.Tokens.Jwt/6.35.0 | Transitive dependency of EF Core SQL Server or EF Core design tooling | 2026-10-08 |
| System.IO.Pipelines | NuGet | 6.0.3 | MIT | https://www.nuget.org/packages/System.IO.Pipelines/6.0.3 | Transitive dependency of EF Core SQL Server or EF Core design tooling | 2026-10-08 |
| System.Memory.Data | NuGet | 1.0.2 | MIT | https://www.nuget.org/packages/System.Memory.Data/1.0.2 | Transitive dependency of EF Core SQL Server or EF Core design tooling | 2026-10-08 |
| System.Memory | NuGet | 4.5.4 | MIT | https://www.nuget.org/packages/System.Memory/4.5.4 | Transitive dependency of EF Core SQL Server or EF Core design tooling | 2026-10-08 |
| System.Numerics.Vectors | NuGet | 4.5.0 | MIT | https://www.nuget.org/packages/System.Numerics.Vectors/4.5.0 | Transitive dependency of EF Core SQL Server or EF Core design tooling | 2026-10-08 |
| System.Reflection.Metadata | NuGet | 6.0.1 | MIT | https://www.nuget.org/packages/System.Reflection.Metadata/6.0.1 | Transitive dependency of EF Core SQL Server or EF Core design tooling | 2026-10-08 |
| System.Runtime.Caching | NuGet | 6.0.0 | MIT | https://www.nuget.org/packages/System.Runtime.Caching/6.0.0 | Transitive dependency of EF Core SQL Server or EF Core design tooling | 2026-10-08 |
| System.Runtime.CompilerServices.Unsafe | NuGet | 6.0.0 | MIT | https://www.nuget.org/packages/System.Runtime.CompilerServices.Unsafe/6.0.0 | Transitive dependency of EF Core SQL Server or EF Core design tooling | 2026-10-08 |
| System.Runtime | NuGet | 4.3.0 | MIT | https://www.nuget.org/packages/System.Runtime/4.3.0 | Transitive dependency of EF Core SQL Server or EF Core design tooling | 2026-10-08 |
| System.Security.AccessControl | NuGet | 6.0.0 | MIT | https://www.nuget.org/packages/System.Security.AccessControl/6.0.0 | Transitive dependency of EF Core SQL Server or EF Core design tooling | 2026-10-08 |
| System.Security.Cryptography.Cng | NuGet | 4.5.0 | MIT | https://www.nuget.org/packages/System.Security.Cryptography.Cng/4.5.0 | Transitive dependency of EF Core SQL Server or EF Core design tooling | 2026-10-08 |
| System.Security.Cryptography.ProtectedData | NuGet | 6.0.0 | MIT | https://www.nuget.org/packages/System.Security.Cryptography.ProtectedData/6.0.0 | Transitive dependency of EF Core SQL Server or EF Core design tooling | 2026-10-08 |
| System.Security.Permissions | NuGet | 6.0.0 | MIT | https://www.nuget.org/packages/System.Security.Permissions/6.0.0 | Transitive dependency of EF Core SQL Server or EF Core design tooling | 2026-10-08 |
| System.Security.Principal.Windows | NuGet | 5.0.0 | MIT | https://www.nuget.org/packages/System.Security.Principal.Windows/5.0.0 | Transitive dependency of EF Core SQL Server or EF Core design tooling | 2026-10-08 |
| System.Text.Encoding.CodePages | NuGet | 6.0.0 | MIT | https://www.nuget.org/packages/System.Text.Encoding.CodePages/6.0.0 | Transitive dependency of EF Core SQL Server or EF Core design tooling | 2026-10-08 |
| System.Text.Encoding | NuGet | 4.3.0 | MIT | https://www.nuget.org/packages/System.Text.Encoding/4.3.0 | Transitive dependency of EF Core SQL Server or EF Core design tooling | 2026-10-08 |
| System.Text.Encodings.Web | NuGet | 4.7.2 | MIT | https://www.nuget.org/packages/System.Text.Encodings.Web/4.7.2 | Transitive dependency of EF Core SQL Server or EF Core design tooling | 2026-10-08 |
| System.Text.Json | NuGet | 4.7.2 | MIT | https://www.nuget.org/packages/System.Text.Json/4.7.2 | Transitive dependency of EF Core SQL Server or EF Core design tooling | 2026-10-08 |
| System.Threading.Channels | NuGet | 6.0.0 | MIT | https://www.nuget.org/packages/System.Threading.Channels/6.0.0 | Transitive dependency of EF Core SQL Server or EF Core design tooling | 2026-10-08 |
| System.Threading.Tasks.Extensions | NuGet | 4.5.4 | MIT | https://www.nuget.org/packages/System.Threading.Tasks.Extensions/4.5.4 | Transitive dependency of EF Core SQL Server or EF Core design tooling | 2026-10-08 |
| System.Windows.Extensions | NuGet | 6.0.0 | MIT | https://www.nuget.org/packages/System.Windows.Extensions/6.0.0 | Transitive dependency of EF Core SQL Server or EF Core design tooling | 2026-10-08 |
| dotnet-ef | .NET local tool | 8.0.31 | MIT | https://www.nuget.org/packages/dotnet-ef/8.0.31 | Repository-local EF Core migration CLI | 2026-10-08 |
| Microsoft.Bcl.AsyncInterfaces | NuGet | 1.1.1 | MIT | https://www.nuget.org/packages/Microsoft.Bcl.AsyncInterfaces/1.1.1/License | Transitive dependency resolved by the current backend or test projects | 2026-10-08 |
| Microsoft.CodeCoverage | NuGet | 17.8.0 | MIT | https://www.nuget.org/packages/Microsoft.CodeCoverage/17.8.0/License | Transitive dependency resolved by the current backend or test projects | 2026-10-08 |
| Microsoft.TestPlatform.ObjectModel | NuGet | 17.8.0 | MIT | https://www.nuget.org/packages/Microsoft.TestPlatform.ObjectModel/17.8.0/License | Transitive dependency resolved by the current backend or test projects | 2026-10-08 |
| Microsoft.TestPlatform.TestHost | NuGet | 17.8.0 | MIT | https://www.nuget.org/packages/Microsoft.TestPlatform.TestHost/17.8.0/License | Transitive dependency resolved by the current backend or test projects | 2026-10-08 |
| Microsoft.Win32.Primitives | NuGet | 4.3.0 | MIT | https://www.nuget.org/packages/Microsoft.Win32.Primitives/4.3.0/License | Transitive dependency resolved by the current backend or test projects | 2026-10-08 |
| NETStandard.Library | NuGet | 1.6.1 | MIT | https://www.nuget.org/packages/NETStandard.Library/1.6.1/License | Transitive dependency resolved by the current backend or test projects | 2026-10-08 |
| Newtonsoft.Json | NuGet | 13.0.1 | MIT | https://www.nuget.org/packages/Newtonsoft.Json/13.0.1/License | Transitive dependency resolved by the current backend or test projects | 2026-10-08 |
| NuGet.Frameworks | NuGet | 6.5.0 | Apache-2.0 | https://www.nuget.org/packages/NuGet.Frameworks/6.5.0/License | Transitive dependency resolved by the current backend or test projects | 2026-10-08 |
| runtime.debian.8-x64.runtime.native.System.Security.Cryptography.OpenSsl | NuGet | 4.3.0 | MIT | https://www.nuget.org/packages/runtime.debian.8-x64.runtime.native.System.Security.Cryptography.OpenSsl/4.3.0/License | Transitive dependency resolved by the current backend or test projects | 2026-10-08 |
| runtime.fedora.23-x64.runtime.native.System.Security.Cryptography.OpenSsl | NuGet | 4.3.0 | MIT | https://www.nuget.org/packages/runtime.fedora.23-x64.runtime.native.System.Security.Cryptography.OpenSsl/4.3.0/License | Transitive dependency resolved by the current backend or test projects | 2026-10-08 |
| runtime.fedora.24-x64.runtime.native.System.Security.Cryptography.OpenSsl | NuGet | 4.3.0 | MIT | https://www.nuget.org/packages/runtime.fedora.24-x64.runtime.native.System.Security.Cryptography.OpenSsl/4.3.0/License | Transitive dependency resolved by the current backend or test projects | 2026-10-08 |
| runtime.native.System.IO.Compression | NuGet | 4.3.0 | MIT | https://www.nuget.org/packages/runtime.native.System.IO.Compression/4.3.0/License | Transitive dependency resolved by the current backend or test projects | 2026-10-08 |
| runtime.native.System.Net.Http | NuGet | 4.3.0 | MIT | https://www.nuget.org/packages/runtime.native.System.Net.Http/4.3.0/License | Transitive dependency resolved by the current backend or test projects | 2026-10-08 |
| runtime.native.System.Security.Cryptography.Apple | NuGet | 4.3.0 | MIT | https://www.nuget.org/packages/runtime.native.System.Security.Cryptography.Apple/4.3.0/License | Transitive dependency resolved by the current backend or test projects | 2026-10-08 |
| runtime.native.System.Security.Cryptography.OpenSsl | NuGet | 4.3.0 | MIT | https://www.nuget.org/packages/runtime.native.System.Security.Cryptography.OpenSsl/4.3.0/License | Transitive dependency resolved by the current backend or test projects | 2026-10-08 |
| runtime.native.System | NuGet | 4.3.0 | MIT | https://www.nuget.org/packages/runtime.native.System/4.3.0/License | Transitive dependency resolved by the current backend or test projects | 2026-10-08 |
| runtime.opensuse.13.2-x64.runtime.native.System.Security.Cryptography.OpenSsl | NuGet | 4.3.0 | MIT | https://www.nuget.org/packages/runtime.opensuse.13.2-x64.runtime.native.System.Security.Cryptography.OpenSsl/4.3.0/License | Transitive dependency resolved by the current backend or test projects | 2026-10-08 |
| runtime.opensuse.42.1-x64.runtime.native.System.Security.Cryptography.OpenSsl | NuGet | 4.3.0 | MIT | https://www.nuget.org/packages/runtime.opensuse.42.1-x64.runtime.native.System.Security.Cryptography.OpenSsl/4.3.0/License | Transitive dependency resolved by the current backend or test projects | 2026-10-08 |
| runtime.osx.10.10-x64.runtime.native.System.Security.Cryptography.Apple | NuGet | 4.3.0 | MIT | https://www.nuget.org/packages/runtime.osx.10.10-x64.runtime.native.System.Security.Cryptography.Apple/4.3.0/License | Transitive dependency resolved by the current backend or test projects | 2026-10-08 |
| runtime.osx.10.10-x64.runtime.native.System.Security.Cryptography.OpenSsl | NuGet | 4.3.0 | MIT | https://www.nuget.org/packages/runtime.osx.10.10-x64.runtime.native.System.Security.Cryptography.OpenSsl/4.3.0/License | Transitive dependency resolved by the current backend or test projects | 2026-10-08 |
| runtime.rhel.7-x64.runtime.native.System.Security.Cryptography.OpenSsl | NuGet | 4.3.0 | MIT | https://www.nuget.org/packages/runtime.rhel.7-x64.runtime.native.System.Security.Cryptography.OpenSsl/4.3.0/License | Transitive dependency resolved by the current backend or test projects | 2026-10-08 |
| runtime.ubuntu.14.04-x64.runtime.native.System.Security.Cryptography.OpenSsl | NuGet | 4.3.0 | MIT | https://www.nuget.org/packages/runtime.ubuntu.14.04-x64.runtime.native.System.Security.Cryptography.OpenSsl/4.3.0/License | Transitive dependency resolved by the current backend or test projects | 2026-10-08 |
| runtime.ubuntu.16.04-x64.runtime.native.System.Security.Cryptography.OpenSsl | NuGet | 4.3.0 | MIT | https://www.nuget.org/packages/runtime.ubuntu.16.04-x64.runtime.native.System.Security.Cryptography.OpenSsl/4.3.0/License | Transitive dependency resolved by the current backend or test projects | 2026-10-08 |
| runtime.ubuntu.16.10-x64.runtime.native.System.Security.Cryptography.OpenSsl | NuGet | 4.3.0 | MIT | https://www.nuget.org/packages/runtime.ubuntu.16.10-x64.runtime.native.System.Security.Cryptography.OpenSsl/4.3.0/License | Transitive dependency resolved by the current backend or test projects | 2026-10-08 |
| System.AppContext | NuGet | 4.3.0 | MIT | https://www.nuget.org/packages/System.AppContext/4.3.0/License | Transitive dependency resolved by the current backend or test projects | 2026-10-08 |
| System.Buffers | NuGet | 4.3.0 | MIT | https://www.nuget.org/packages/System.Buffers/4.3.0/License | Transitive dependency resolved by the current backend or test projects | 2026-10-08 |
| System.Collections.Concurrent | NuGet | 4.3.0 | MIT | https://www.nuget.org/packages/System.Collections.Concurrent/4.3.0/License | Transitive dependency resolved by the current backend or test projects | 2026-10-08 |
| System.Collections | NuGet | 4.3.0 | MIT | https://www.nuget.org/packages/System.Collections/4.3.0/License | Transitive dependency resolved by the current backend or test projects | 2026-10-08 |
| System.Console | NuGet | 4.3.0 | MIT | https://www.nuget.org/packages/System.Console/4.3.0/License | Transitive dependency resolved by the current backend or test projects | 2026-10-08 |
| System.Diagnostics.Debug | NuGet | 4.3.0 | MIT | https://www.nuget.org/packages/System.Diagnostics.Debug/4.3.0/License | Transitive dependency resolved by the current backend or test projects | 2026-10-08 |
| System.Diagnostics.Tools | NuGet | 4.3.0 | MIT | https://www.nuget.org/packages/System.Diagnostics.Tools/4.3.0/License | Transitive dependency resolved by the current backend or test projects | 2026-10-08 |
| System.Diagnostics.Tracing | NuGet | 4.3.0 | MIT | https://www.nuget.org/packages/System.Diagnostics.Tracing/4.3.0/License | Transitive dependency resolved by the current backend or test projects | 2026-10-08 |
| System.Globalization.Calendars | NuGet | 4.3.0 | MIT | https://www.nuget.org/packages/System.Globalization.Calendars/4.3.0/License | Transitive dependency resolved by the current backend or test projects | 2026-10-08 |
| System.Globalization.Extensions | NuGet | 4.3.0 | MIT | https://www.nuget.org/packages/System.Globalization.Extensions/4.3.0/License | Transitive dependency resolved by the current backend or test projects | 2026-10-08 |
| System.Globalization | NuGet | 4.3.0 | MIT | https://www.nuget.org/packages/System.Globalization/4.3.0/License | Transitive dependency resolved by the current backend or test projects | 2026-10-08 |
| System.IO.Compression.ZipFile | NuGet | 4.3.0 | MIT | https://www.nuget.org/packages/System.IO.Compression.ZipFile/4.3.0/License | Transitive dependency resolved by the current backend or test projects | 2026-10-08 |
| System.IO.Compression | NuGet | 4.3.0 | MIT | https://www.nuget.org/packages/System.IO.Compression/4.3.0/License | Transitive dependency resolved by the current backend or test projects | 2026-10-08 |
| System.IO.FileSystem.Primitives | NuGet | 4.3.0 | MIT | https://www.nuget.org/packages/System.IO.FileSystem.Primitives/4.3.0/License | Transitive dependency resolved by the current backend or test projects | 2026-10-08 |
| System.IO.FileSystem | NuGet | 4.3.0 | MIT | https://www.nuget.org/packages/System.IO.FileSystem/4.3.0/License | Transitive dependency resolved by the current backend or test projects | 2026-10-08 |
| System.IO | NuGet | 4.3.0 | MIT | https://www.nuget.org/packages/System.IO/4.3.0/License | Transitive dependency resolved by the current backend or test projects | 2026-10-08 |
| System.Linq.Expressions | NuGet | 4.3.0 | MIT | https://www.nuget.org/packages/System.Linq.Expressions/4.3.0/License | Transitive dependency resolved by the current backend or test projects | 2026-10-08 |
| System.Linq | NuGet | 4.3.0 | MIT | https://www.nuget.org/packages/System.Linq/4.3.0/License | Transitive dependency resolved by the current backend or test projects | 2026-10-08 |
| System.Net.Http | NuGet | 4.3.0 | MIT | https://www.nuget.org/packages/System.Net.Http/4.3.0/License | Transitive dependency resolved by the current backend or test projects | 2026-10-08 |
| System.Net.Primitives | NuGet | 4.3.0 | MIT | https://www.nuget.org/packages/System.Net.Primitives/4.3.0/License | Transitive dependency resolved by the current backend or test projects | 2026-10-08 |
| System.Net.Sockets | NuGet | 4.3.0 | MIT | https://www.nuget.org/packages/System.Net.Sockets/4.3.0/License | Transitive dependency resolved by the current backend or test projects | 2026-10-08 |
| System.ObjectModel | NuGet | 4.3.0 | MIT | https://www.nuget.org/packages/System.ObjectModel/4.3.0/License | Transitive dependency resolved by the current backend or test projects | 2026-10-08 |
| System.Reflection.Emit.ILGeneration | NuGet | 4.3.0 | MIT | https://www.nuget.org/packages/System.Reflection.Emit.ILGeneration/4.3.0/License | Transitive dependency resolved by the current backend or test projects | 2026-10-08 |
| System.Reflection.Emit.Lightweight | NuGet | 4.3.0 | MIT | https://www.nuget.org/packages/System.Reflection.Emit.Lightweight/4.3.0/License | Transitive dependency resolved by the current backend or test projects | 2026-10-08 |
| System.Reflection.Emit | NuGet | 4.3.0 | MIT | https://www.nuget.org/packages/System.Reflection.Emit/4.3.0/License | Transitive dependency resolved by the current backend or test projects | 2026-10-08 |
| System.Reflection.Extensions | NuGet | 4.3.0 | MIT | https://www.nuget.org/packages/System.Reflection.Extensions/4.3.0/License | Transitive dependency resolved by the current backend or test projects | 2026-10-08 |
| System.Reflection.Metadata | NuGet | 1.6.0 | MIT | https://www.nuget.org/packages/System.Reflection.Metadata/1.6.0/License | Transitive dependency resolved by the current backend or test projects | 2026-10-08 |
| System.Reflection.Primitives | NuGet | 4.3.0 | MIT | https://www.nuget.org/packages/System.Reflection.Primitives/4.3.0/License | Transitive dependency resolved by the current backend or test projects | 2026-10-08 |
| System.Reflection.TypeExtensions | NuGet | 4.3.0 | MIT | https://www.nuget.org/packages/System.Reflection.TypeExtensions/4.3.0/License | Transitive dependency resolved by the current backend or test projects | 2026-10-08 |
| System.Reflection | NuGet | 4.3.0 | MIT | https://www.nuget.org/packages/System.Reflection/4.3.0/License | Transitive dependency resolved by the current backend or test projects | 2026-10-08 |
| System.Resources.ResourceManager | NuGet | 4.3.0 | MIT | https://www.nuget.org/packages/System.Resources.ResourceManager/4.3.0/License | Transitive dependency resolved by the current backend or test projects | 2026-10-08 |
| System.Runtime.Extensions | NuGet | 4.3.0 | MIT | https://www.nuget.org/packages/System.Runtime.Extensions/4.3.0/License | Transitive dependency resolved by the current backend or test projects | 2026-10-08 |
| System.Runtime.Handles | NuGet | 4.3.0 | MIT | https://www.nuget.org/packages/System.Runtime.Handles/4.3.0/License | Transitive dependency resolved by the current backend or test projects | 2026-10-08 |
| System.Runtime.InteropServices.RuntimeInformation | NuGet | 4.3.0 | MIT | https://www.nuget.org/packages/System.Runtime.InteropServices.RuntimeInformation/4.3.0/License | Transitive dependency resolved by the current backend or test projects | 2026-10-08 |
| System.Runtime.InteropServices | NuGet | 4.3.0 | MIT | https://www.nuget.org/packages/System.Runtime.InteropServices/4.3.0/License | Transitive dependency resolved by the current backend or test projects | 2026-10-08 |
| System.Runtime.Numerics | NuGet | 4.3.0 | MIT | https://www.nuget.org/packages/System.Runtime.Numerics/4.3.0/License | Transitive dependency resolved by the current backend or test projects | 2026-10-08 |
| System.Security.Cryptography.Algorithms | NuGet | 4.3.0 | MIT | https://www.nuget.org/packages/System.Security.Cryptography.Algorithms/4.3.0/License | Transitive dependency resolved by the current backend or test projects | 2026-10-08 |
| System.Security.Cryptography.Csp | NuGet | 4.3.0 | MIT | https://www.nuget.org/packages/System.Security.Cryptography.Csp/4.3.0/License | Transitive dependency resolved by the current backend or test projects | 2026-10-08 |
| System.Security.Cryptography.Encoding | NuGet | 4.3.0 | MIT | https://www.nuget.org/packages/System.Security.Cryptography.Encoding/4.3.0/License | Transitive dependency resolved by the current backend or test projects | 2026-10-08 |
| System.Security.Cryptography.OpenSsl | NuGet | 4.3.0 | MIT | https://www.nuget.org/packages/System.Security.Cryptography.OpenSsl/4.3.0/License | Transitive dependency resolved by the current backend or test projects | 2026-10-08 |
| System.Security.Cryptography.Primitives | NuGet | 4.3.0 | MIT | https://www.nuget.org/packages/System.Security.Cryptography.Primitives/4.3.0/License | Transitive dependency resolved by the current backend or test projects | 2026-10-08 |
| System.Security.Cryptography.X509Certificates | NuGet | 4.3.0 | MIT | https://www.nuget.org/packages/System.Security.Cryptography.X509Certificates/4.3.0/License | Transitive dependency resolved by the current backend or test projects | 2026-10-08 |
| System.Text.Encoding.Extensions | NuGet | 4.3.0 | MIT | https://www.nuget.org/packages/System.Text.Encoding.Extensions/4.3.0/License | Transitive dependency resolved by the current backend or test projects | 2026-10-08 |
| System.Text.RegularExpressions | NuGet | 4.3.0 | MIT | https://www.nuget.org/packages/System.Text.RegularExpressions/4.3.0/License | Transitive dependency resolved by the current backend or test projects | 2026-10-08 |
| System.Threading.Tasks | NuGet | 4.3.0 | MIT | https://www.nuget.org/packages/System.Threading.Tasks/4.3.0/License | Transitive dependency resolved by the current backend or test projects | 2026-10-08 |
| System.Threading.Timer | NuGet | 4.3.0 | MIT | https://www.nuget.org/packages/System.Threading.Timer/4.3.0/License | Transitive dependency resolved by the current backend or test projects | 2026-10-08 |
| System.Threading | NuGet | 4.3.0 | MIT | https://www.nuget.org/packages/System.Threading/4.3.0/License | Transitive dependency resolved by the current backend or test projects | 2026-10-08 |
| System.Xml.ReaderWriter | NuGet | 4.3.0 | MIT | https://www.nuget.org/packages/System.Xml.ReaderWriter/4.3.0/License | Transitive dependency resolved by the current backend or test projects | 2026-10-08 |
| System.Xml.XDocument | NuGet | 4.3.0 | MIT | https://www.nuget.org/packages/System.Xml.XDocument/4.3.0/License | Transitive dependency resolved by the current backend or test projects | 2026-10-08 |
| xunit.abstractions | NuGet | 2.0.3 | Apache-2.0 | https://www.nuget.org/packages/xunit.abstractions/2.0.3/License | Transitive dependency resolved by the current backend or test projects | 2026-10-08 |
| xunit.analyzers | NuGet | 1.4.0 | Apache-2.0 | https://www.nuget.org/packages/xunit.analyzers/1.4.0/License | Transitive dependency resolved by the current backend or test projects | 2026-10-08 |
| xunit.assert | NuGet | 2.5.3 | Apache-2.0 | https://www.nuget.org/packages/xunit.assert/2.5.3/License | Transitive dependency resolved by the current backend or test projects | 2026-10-08 |
| xunit.core | NuGet | 2.5.3 | Apache-2.0 | https://www.nuget.org/packages/xunit.core/2.5.3/License | Transitive dependency resolved by the current backend or test projects | 2026-10-08 |
| xunit.extensibility.core | NuGet | 2.5.3 | Apache-2.0 | https://www.nuget.org/packages/xunit.extensibility.core/2.5.3/License | Transitive dependency resolved by the current backend or test projects | 2026-10-08 |
| xunit.extensibility.execution | NuGet | 2.5.3 | Apache-2.0 | https://www.nuget.org/packages/xunit.extensibility.execution/2.5.3/License | Transitive dependency resolved by the current backend or test projects | 2026-10-08 |
| Microsoft.AspNetCore.Authentication.JwtBearer | NuGet | 8.0.31 | MIT | https://www.nuget.org/packages/Microsoft.AspNetCore.Authentication.JwtBearer/8.0.31/License | JWT bearer authentication middleware | 2026-10-09 |
| Microsoft.AspNetCore.Identity.EntityFrameworkCore | NuGet | 8.0.31 | MIT | https://www.nuget.org/packages/Microsoft.AspNetCore.Identity.EntityFrameworkCore/8.0.31/License | ASP.NET Core Identity persistence with EF Core | 2026-10-09 |
| Microsoft.AspNetCore.Cryptography.Internal | NuGet | 8.0.31 | MIT | https://www.nuget.org/packages/Microsoft.AspNetCore.Cryptography.Internal/8.0.31/License | Transitive dependency of ASP.NET Core Identity | 2026-10-09 |
| Microsoft.AspNetCore.Cryptography.KeyDerivation | NuGet | 8.0.31 | MIT | https://www.nuget.org/packages/Microsoft.AspNetCore.Cryptography.KeyDerivation/8.0.31/License | Transitive dependency of ASP.NET Core Identity password hashing | 2026-10-09 |
| Microsoft.EntityFrameworkCore.InMemory | NuGet | 8.0.31 | MIT | https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.InMemory/8.0.31/License | Isolated authentication service tests | 2026-10-09 |
| Microsoft.Extensions.Identity.Core | NuGet | 8.0.31 | MIT | https://www.nuget.org/packages/Microsoft.Extensions.Identity.Core/8.0.31/License | Transitive ASP.NET Core Identity services | 2026-10-09 |
| Microsoft.Extensions.Identity.Stores | NuGet | 8.0.31 | MIT | https://www.nuget.org/packages/Microsoft.Extensions.Identity.Stores/8.0.31/License | Identity user model and store abstractions | 2026-10-09 |
| Microsoft.IdentityModel.Abstractions | NuGet | 7.7.3 | MIT | https://www.nuget.org/packages/Microsoft.IdentityModel.Abstractions/7.7.3/License | Transitive JWT abstraction support | 2026-10-09 |
| Microsoft.IdentityModel.JsonWebTokens | NuGet | 7.7.3 | MIT | https://www.nuget.org/packages/Microsoft.IdentityModel.JsonWebTokens/7.7.3/License | Transitive JWT processing support | 2026-10-09 |
| Microsoft.IdentityModel.Logging | NuGet | 7.7.3 | MIT | https://www.nuget.org/packages/Microsoft.IdentityModel.Logging/7.7.3/License | Transitive JWT diagnostics support | 2026-10-09 |
| Microsoft.IdentityModel.Protocols | NuGet | 7.7.3 | MIT | https://www.nuget.org/packages/Microsoft.IdentityModel.Protocols/7.7.3/License | Transitive token protocol support | 2026-10-09 |
| Microsoft.IdentityModel.Protocols.OpenIdConnect | NuGet | 7.7.3 | MIT | https://www.nuget.org/packages/Microsoft.IdentityModel.Protocols.OpenIdConnect/7.7.3/License | Transitive JWT bearer protocol support | 2026-10-09 |
| Microsoft.IdentityModel.Tokens | NuGet | 7.7.3 | MIT | https://www.nuget.org/packages/Microsoft.IdentityModel.Tokens/7.7.3/License | JWT signing and validation primitives | 2026-10-09 |
| System.IdentityModel.Tokens.Jwt | NuGet | 7.7.3 | MIT | https://www.nuget.org/packages/System.IdentityModel.Tokens.Jwt/7.7.3/License | JWT creation and serialization | 2026-10-09 |
| flutter_secure_storage | pub.dev | 11.2.0 | BSD-3-Clause | https://pub.dev/packages/flutter_secure_storage/versions/11.2.0 | Encrypted mobile refresh-token storage | 2026-10-09 |
| flutter_secure_storage_darwin | pub.dev | 0.4.3 | BSD-3-Clause | https://pub.dev/packages/flutter_secure_storage_darwin/versions/0.4.3 | Apple Keychain implementation for flutter_secure_storage | 2026-10-09 |
| flutter_secure_storage_linux | pub.dev | 3.0.3 | BSD-3-Clause | https://pub.dev/packages/flutter_secure_storage_linux/versions/3.0.3 | Transitive Linux implementation of flutter_secure_storage | 2026-10-09 |
| flutter_secure_storage_platform_interface | pub.dev | 2.1.1 | BSD-3-Clause | https://pub.dev/packages/flutter_secure_storage_platform_interface/versions/2.1.1 | Platform interface for flutter_secure_storage | 2026-10-09 |
| flutter_secure_storage_web | pub.dev | 2.1.1 | BSD-3-Clause | https://pub.dev/packages/flutter_secure_storage_web/versions/2.1.1 | Transitive web implementation resolved by flutter_secure_storage | 2026-10-09 |
| flutter_secure_storage_windows | pub.dev | 4.2.2 | BSD-3-Clause | https://pub.dev/packages/flutter_secure_storage_windows/versions/4.2.2 | Transitive Windows implementation resolved by flutter_secure_storage | 2026-10-09 |
| ffi | pub.dev | 2.2.0 | BSD-3-Clause | https://pub.dev/packages/ffi/versions/2.2.0 | Transitive native interoperability support | 2026-10-09 |
| ffi_leak_tracker | pub.dev | 0.1.2 | BSD-3-Clause | https://pub.dev/packages/ffi_leak_tracker/versions/0.1.2 | Transitive native resource tracking support | 2026-10-09 |
| plugin_platform_interface | pub.dev | 2.1.8 | BSD-3-Clause | https://pub.dev/packages/plugin_platform_interface/versions/2.1.8 | Flutter plugin platform interface support | 2026-10-09 |
| web | pub.dev | 1.1.1 | BSD-3-Clause | https://pub.dev/packages/web/versions/1.1.1 | Transitive browser API bindings | 2026-10-09 |
| path_provider | pub.dev | 2.1.6 | BSD-3-Clause | https://pub.dev/packages/path_provider/versions/2.1.6 | Transitive filesystem path discovery | 2026-10-09 |
| path_provider_android | pub.dev | 2.3.1 | BSD-3-Clause | https://pub.dev/packages/path_provider_android/versions/2.3.1 | Android implementation of path_provider | 2026-10-09 |
| path_provider_foundation | pub.dev | 2.6.0 | BSD-3-Clause | https://pub.dev/packages/path_provider_foundation/versions/2.6.0 | Apple implementation of path_provider | 2026-10-09 |
| path_provider_linux | pub.dev | 2.2.2 | BSD-3-Clause | https://pub.dev/packages/path_provider_linux/versions/2.2.2 | Transitive Linux implementation of path_provider | 2026-10-09 |
| path_provider_platform_interface | pub.dev | 2.1.3 | BSD-3-Clause | https://pub.dev/packages/path_provider_platform_interface/versions/2.1.3 | Platform interface for path_provider | 2026-10-09 |
| path_provider_windows | pub.dev | 2.3.0 | BSD-3-Clause | https://pub.dev/packages/path_provider_windows/versions/2.3.0 | Transitive Windows implementation of path_provider | 2026-10-09 |
| win32 | pub.dev | 6.4.0 | BSD-3-Clause | https://pub.dev/packages/win32/versions/6.4.0 | Transitive Windows API bindings | 2026-10-09 |
| jni | pub.dev | 1.1.0 | BSD-3-Clause | https://pub.dev/packages/jni/versions/1.1.0 | Transitive Android Java interoperability | 2026-10-09 |
| jni_flutter | pub.dev | 1.0.4+1 | BSD-3-Clause | https://pub.dev/packages/jni_flutter/versions/1.0.4%2B1 | Transitive Flutter integration for JNI | 2026-10-09 |
| jni_util | pub.dev | 1.0.0 | BSD-3-Clause | https://pub.dev/packages/jni_util/versions/1.0.0 | Transitive JNI utilities | 2026-10-09 |
| args | pub.dev | 2.7.0 | BSD-3-Clause | https://pub.dev/packages/args/versions/2.7.0 | Transitive command-line argument support for build tooling | 2026-10-09 |
| package_config | pub.dev | 3.0.0 | BSD-3-Clause | https://pub.dev/packages/package_config/versions/3.0.0 | Transitive Dart package configuration support | 2026-10-09 |
| objective_c | pub.dev | 9.5.0 | BSD-3-Clause | https://pub.dev/packages/objective_c/versions/9.5.0 | Transitive Apple native interoperability | 2026-10-09 |
| code_assets | pub.dev | 1.2.1 | BSD-3-Clause | https://pub.dev/packages/code_assets/versions/1.2.1 | Transitive native code asset build support | 2026-10-09 |
| hooks | pub.dev | 2.0.2 | BSD-3-Clause | https://pub.dev/packages/hooks/versions/2.0.2 | Transitive Dart build hook support | 2026-10-09 |
| logging | pub.dev | 1.3.0 | BSD-3-Clause | https://pub.dev/packages/logging/versions/1.3.0 | Transitive structured logging primitives | 2026-10-09 |
| pub_semver | pub.dev | 2.2.1 | BSD-3-Clause | https://pub.dev/packages/pub_semver/versions/2.2.1 | Transitive semantic version parsing | 2026-10-09 |
| platform | pub.dev | 3.2.0 | BSD-3-Clause | https://pub.dev/packages/platform/versions/3.2.0 | Transitive host platform detection | 2026-10-09 |
| xdg_directories | pub.dev | 1.1.0 | BSD-3-Clause | https://pub.dev/packages/xdg_directories/versions/1.1.0 | Transitive Linux directory discovery | 2026-10-09 |
| record_use | pub.dev | 0.6.0 | BSD-3-Clause | https://pub.dev/packages/record_use/versions/0.6.0 | Transitive build-time record usage support | 2026-10-09 |
| crypto | pub.dev | 3.0.7 | BSD-3-Clause | https://pub.dev/packages/crypto/versions/3.0.7 | Transitive cryptographic primitives | 2026-10-09 |
| yaml | pub.dev | 3.1.4 | MIT | https://pub.dev/packages/yaml/versions/3.1.4 | Transitive YAML parsing for build tooling | 2026-10-09 |
| typed_data | pub.dev | 1.4.0 | BSD-3-Clause | https://pub.dev/packages/typed_data/versions/1.4.0 | Transitive typed byte buffer utilities | 2026-10-09 |
| collection | pub.dev | 1.19.1 | BSD-3-Clause | https://pub.dev/packages/collection/versions/1.19.1 | Transitive collection utilities | 2026-10-09 |
| meta | pub.dev | 1.18.3 | BSD-3-Clause | https://pub.dev/packages/meta/versions/1.18.3 | Transitive Dart annotations | 2026-10-09 |
| path | pub.dev | 1.9.1 | BSD-3-Clause | https://pub.dev/packages/path/versions/1.9.1 | Transitive cross-platform path utilities | 2026-10-09 |
| source_span | pub.dev | 1.10.2 | BSD-3-Clause | https://pub.dev/packages/source_span/versions/1.10.2 | Transitive source location utilities | 2026-10-09 |
| string_scanner | pub.dev | 1.4.1 | BSD-3-Clause | https://pub.dev/packages/string_scanner/versions/1.4.1 | Transitive string scanning utilities | 2026-10-09 |
| term_glyph | pub.dev | 1.2.2 | BSD-3-Clause | https://pub.dev/packages/term_glyph/versions/1.2.2 | Transitive terminal glyph support | 2026-10-09 |
| Flutter SDK (`flutter_localizations`) | Flutter SDK | 3.47.5 | BSD-3-Clause | https://github.com/flutter/flutter/blob/3.47.5/LICENSE | SDK localization support required transitively by Flutter UI packages | 2026-10-10 |
| flutter_riverpod | pub.dev | 3.4.3 | MIT | https://pub.dev/packages/flutter_riverpod/versions/3.4.3 | Flutter state management and dependency injection | 2026-10-10 |
| riverpod | pub.dev | 3.4.3 | MIT | https://pub.dev/packages/riverpod/versions/3.4.3 | Core state management used by flutter_riverpod | 2026-10-10 |
| state_notifier | pub.dev | 1.0.0 | MIT | https://pub.dev/packages/state_notifier/versions/1.0.0 | Transitive observable state support for Riverpod | 2026-10-10 |
| listen | pub.dev | 1.0.1 | BSD-3-Clause | https://pub.dev/packages/listen/versions/1.0.1 | Transitive listener primitives for Riverpod | 2026-10-10 |
| uuid | pub.dev | 4.6.0 | MIT | https://pub.dev/packages/uuid/versions/4.6.0 | Transitive identifier support for Riverpod | 2026-10-10 |
| fixnum | pub.dev | 1.1.1 | BSD-3-Clause | https://pub.dev/packages/fixnum/versions/1.1.1 | Transitive fixed-width integer support for uuid | 2026-10-10 |
| go_router | pub.dev | 18.0.2 | BSD-3-Clause | https://pub.dev/packages/go_router/versions/18.0.2 | Declarative mobile navigation | 2026-10-10 |
| cupertino_ui | pub.dev | 1.1.2 | BSD-3-Clause | https://pub.dev/packages/cupertino_ui/versions/1.1.2 | Transitive Cupertino UI support for go_router and material_ui | 2026-10-10 |
| material_ui | pub.dev | 1.6.0 | BSD-3-Clause | https://pub.dev/packages/material_ui/versions/1.6.0 | Transitive Material UI support for go_router | 2026-10-10 |
| intl | pub.dev | 0.20.3 | BSD-3-Clause | https://pub.dev/packages/intl/versions/0.20.3 | Transitive internationalization support for Flutter UI packages | 2026-10-10 |
| dio | pub.dev | 5.11.1 | MIT | https://pub.dev/packages/dio/versions/5.11.1 | REST API client with cancellation and interceptors | 2026-10-10 |
| dio_web_adapter | pub.dev | 2.2.2 | MIT | https://pub.dev/packages/dio_web_adapter/versions/2.2.2 | Transitive Dio platform adapter resolved by the package | 2026-10-10 |
| http_parser | pub.dev | 4.1.2 | BSD-3-Clause | https://pub.dev/packages/http_parser/versions/4.1.2 | Transitive HTTP media type parsing for Dio | 2026-10-10 |
| mime | pub.dev | 2.1.0 | BSD-3-Clause | https://pub.dev/packages/mime/versions/2.1.0 | Transitive MIME type support for Dio | 2026-10-10 |
| async | pub.dev | 2.13.1 | BSD-3-Clause | https://pub.dev/packages/async/versions/2.13.1 | Transitive asynchronous utilities for Riverpod and Dio | 2026-10-10 |
| clock | pub.dev | 1.1.3 | Apache-2.0 | https://pub.dev/packages/clock/versions/1.1.3 | Transitive clock abstraction for Riverpod | 2026-10-10 |
| test_api | pub.dev | 0.7.12 | BSD-3-Clause | https://pub.dev/packages/test_api/versions/0.7.12 | Transitive testing primitives required by Riverpod | 2026-10-10 |
| material_color_utilities | pub.dev | 0.13.0 | Apache-2.0 | https://pub.dev/packages/material_color_utilities/versions/0.13.0 | Transitive Material color support | 2026-10-10 |
| vector_math | pub.dev | 2.4.0 | BSD-3-Clause | https://pub.dev/packages/vector_math/versions/2.4.0 | Transitive vector utilities for Material UI | 2026-10-10 |
