# NuGet transitive dependency demo

This prototype demonstrates a project-reference build and the same call chain
after publishing two packages to a local NuGet feed. It does not publish to
nuget.org.

## Layout

- `ProjectReferences` contains solution 1: the console app references Library A,
  and Library A references Library B as projects.
- `NuGetConsumer` contains solution 2: the console app references only the
  `NuGetDemo.LibraryA` package. Library A's package metadata declares
  `NuGetDemo.LibraryB` as a dependency.
- Both solutions use the `.slnx` format. The consumer's package version is
  centrally managed in `Directory.Packages.props`.
- `NuGetFeed` is the local NuGet source configured in `NuGet.Config`.

Both libraries are version `1.0.0`.

## Run the project-reference solution

From this directory:

```powershell
dotnet run --project .\ProjectReferences\DemoApp\DemoApp.csproj
```

Expected output:

```text
Hello from Library B, called through Library A.
```

## Pack and run the NuGet consumer

Pack B first, then A, into the local feed:

```powershell
dotnet pack .\ProjectReferences\LibraryB\LibraryB.csproj --configuration Release --output .\NuGetFeed
dotnet pack .\ProjectReferences\LibraryA\LibraryA.csproj --configuration Release --output .\NuGetFeed
```

Run solution 2. NuGet restores A and its transitive B dependency from the local
feed:

```powershell
dotnet run --project .\NuGetConsumer\NuGetConsumerApp\NuGetConsumerApp.csproj
```

To confirm both packages were resolved:

```powershell
dotnet list .\NuGetConsumer\NuGetConsumerApp\NuGetConsumerApp.csproj package --include-transitive
```

The output should list `NuGetDemo.LibraryA` and `NuGetDemo.LibraryB`, both at
version `1.0.0`. The consumer project contains no project references.
"# package-demo" 
