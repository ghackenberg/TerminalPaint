# 📖 Lesson 0: Console basics

TODO

![Illustration of a console window with columns and rows.](../../Drawings/Console%20Window%20Width%20and%20Height.jpg)

TODO

```csharp
namespace TerminalPaint {
    internal class Program {
        static void Main(string[] args) {
            // Step 1: Clear screen
            // Step 2: Paint borders
            // Step 3: Paint pointer
        }
    }
}
```

TODO

## Step 1: Clear screen

TODO

```csharp
Console.Clear();
```

TODO

![Screenshot of console window after clearing](./Screenshot_0.png)

## Step 2: Paint borders

TODO

```csharp
Console.BackgroundColor = ConsoleColor.White;
Console.ForegroundColor = ConsoleColor.Black;
```

TODO

```csharp
// Step 2.1: Paint top border
// Step 2.2: Paint left and right border
// Step 2.3: Paint bottom border
```

TODO

### Step 2.1: Paint top border

TODO

```csharp
Console.SetCursorPosition(0, 0);
```

TODO

```csharp
for (int column = 0; column < Console.WindowWidth; column++)
{
    Console.Write(' ');
}
```

TODO

![Screenshot of console window after painting top border](./Screenshot_1.png)

### Step 2.2: Paint left and right border

TODO

```csharp
for (int row = 1; row < Console.WindowHeight - 1; row++)
{
    // Paint left border
    Console.SetCursorPosition(0, row);
    Console.Write(' ');

    // Paint right border
    Console.SetCursorPosition(Console.WindowWidth - 1, row);
    Console.Write(' ');
}
```

TODO

![Screenshot of console window after painting left and right borders](./Screenshot_2.png)

### Step 2.3: Paint bottom border

TODO

```csharp
Console.SetCursorPosition(0, Console.WindowHeight - 1);
```

TODO

```csharp
for (int column = 0; column < Console.WindowWidth; column++)
{
    Console.Write(' ');
}
```

TODO

![Screenshot of console window after painting bottom border](./Screenshot_3.png)

## Step 3: Paint pointer

TODO

```csharp
Console.BackgroundColor = ConsoleColor.Black;
Console.ForegroundColor = ConsoleColor.White;
```

TODO

```csharp
int pointerX = Console.WindowWidth / 2;
int pointerY = Console.WindowHeight / 2;
```

TODO

```csharp
Console.SetCursorPosition(pointerX, pointerY);
Console.Write('X');
```

TODO

![Screenshot of console window after painting cursor in the center](./Screenshot_4.png)