# TerminalPaint

![](./Screenshots/Man%20with%20Hat.png)

## Lessons

TODO

### Console Basics

TODO

![](./Drawings/Console%20Window%20Width%20and%20Height.jpg)

TODO

```csharp
Console.Clear();
```

TODO

```csharp
int centerX = Console.WindowWidth / 2;
int centerY = Console.WindowHeight / 2;
```

TODO

```csharp
Console.SetCursorPosition(centerX, centerY);
```

TODO

```csharp
Console.BackgroundColor = ConsoleColor.Black;
Console.ForegroundColor = ConsoleColor.White;
```

TODO

```csharp
Console.Write('X')
````

### Image Data

TODO

![](./Drawings/Console%20Window%20and%20Image%20Data.jpg)

TODO

```csharp
int imageWidth = Console.WindowWidth - 2;
int imageHeight = Console.WindowHeight - 2;
```

TODO

```csharp
ConsoleColor[] imageData = ConsoleColor[imageWidth * imageHeight];
```

TODO

![](./Drawings/Image%20Data%20Array%20Indexing.jpg)

TODO

```csharp
imageData[y * imageWidth + x] = ConsoleColor.Red;
```

### Color Selection

TODO

![](./Drawings/Color%20Selection.jpg)

## Outlook

TODO

### GUI Object Models

TODO

![](./Drawings/Graphical%20User%20Interface%20Object%20Model.jpg)