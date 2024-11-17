## Запуск проекту

```bash
echo "Building library..."
dotnet build TaskProcessorLib
```

```bash
echo "Packing library..."
dotnet pack TaskProcessorLib -o NugetLocalRepo
```

```bash
echo "Adding NuGet source..."
dotnet nuget add source NugetLocalRepo --name LocalNuget
```

```bash
echo "Building app..."
cd TaskProcessorApp
dotnet add package AKovalskyi --source ../NugetLocalRepo
dotnet run
```
