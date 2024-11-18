# VAR 24

# Lab 1,2,3:

Запустити лабораторну:
```bash
dotnet build Build.proj -t:Run -p:Solution=Lab1
```
Білд:
```bash
dotnet build Build.proj -t:Build -p:Solution=Lab1
```
Тести:
```bash
dotnet build Build.proj -t:Test -p:Solution=Lab1
```

# Lab3

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
