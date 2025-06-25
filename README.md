# TerminalPaint

**TerminalPaint** is a basic paint application running in the terminal.
The application features a color picker as well as loading and saving of images.
The color picker provides access to a small palette of colors supported on a wide range of platforms (e.g. Windows, Linux, and MacOS).
Image loading and saving uses a custom image format, which intentionally is not compatible with other tools.
The application itself is written in C# with .NET Core.

We use this application for teaching programming at the [School of Engineering](https://fh-ooe.at/en/campus-wels) of the [University of Applied Sciences Upper Austria](https://fh-ooe.at/en).
According to our experience, the application is simple enough to be fully unterstood even by beginners, but interesting enough to motivate students to work on the assigments.

*Enjoy! 😉*

![](./Screencasts/Rectangles%20and%20Fill.gif#)

## Screenshots

Here are some screenshots of the application.
The screenshots also document the evolution of the software.
Originally, we had only basic drawing features.
Later we added colors and help text.
And so the application evolved...

### Rectangles and Lines

In this **fourth version**, we added rectangle and line drawing capabilities to the application.

![](./Screenshots/Rectangles%20and%20Lines.png)

### Flower Heart

In this **third version**, we added help text to the software in the top and the bottom borders.

![](./Screenshots/Flower%20Heart%20with%20Textbars.png)

### Rocket

In this **second version** of the software, we added a color palette and selector at the right corner.

![](./Screenshots/Rocket%20with%20Color%20Picker.png)

### Man with Hat

In this **first version**, only basic drawing capabilities were available solely with red color pixels.

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