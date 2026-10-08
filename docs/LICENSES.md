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
