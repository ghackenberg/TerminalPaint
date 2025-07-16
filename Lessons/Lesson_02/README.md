# 📖 Lesson 2: Image representation

TODO

## Console Window and Image Coordinates

TODO

![](../../Drawings/Console%20Window%20and%20Image%20Coordinates%201.jpg)

TODO

![](../../Drawings/Console%20Window%20and%20Image%20Coordinates%202.jpg)

TODO

![](../../Drawings/Console%20Window%20and%20Image%20Coordinates%203.jpg)

## Image Arrays and Pixel Indices

TODO

![](../../Drawings/Console%20Window%20and%20Image%20Data.jpg)

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

![](../../Drawings/Image%20Data%20Array%20Indexing.jpg)

TODO

```csharp
imageData[y * imageWidth + x] = ConsoleColor.Red;
```