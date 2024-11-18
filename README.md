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
# Lab4
![image](https://github.com/user-attachments/assets/a922a84e-cf0c-4f71-ab14-5a8a0e109585)

![image](https://github.com/user-attachments/assets/ae1ffaee-b032-4c7e-b17f-6ce42e5b2439)

![image](https://github.com/user-attachments/assets/38ca7dcc-27a8-4412-9c29-7dc37c584505)

![image](https://github.com/user-attachments/assets/a66d952a-c54a-4c5b-878f-6ace786236f5)
