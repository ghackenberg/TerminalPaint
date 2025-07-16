# 📖 Lesson 2: Image representation

*Previous lesson: [Pointer navigation](../Lesson_01/README.md) | Next lesson: [Color selection](../Lesson_03/README.md)*

In this lesson, we will learn how to represent our drawing area as an "image" in the computer's memory. This is a big step up from just moving a pointer. We will create a data structure that stores the color of every single pixel on our canvas. This will allow us to change the color of pixels and have the program remember those changes. The screencast below shows our goal: we can move the pointer around and, by pressing the spacebar, change the color of the pixels to red.

![Screencast showing the pointer moving around the screen and giving some pixels a red color](./Screencast.gif)

The following code snippet shows the new structure of our program. We've added a step to initialize our image data and we will make several changes to the main loop to handle the new functionality.

```csharp
namespace TerminalPaint {
    internal class Program {
        static void Main(string[] args) {
            // PREVIOUS LESSON:

            // Step 1: Clear screen
            // Step 2: Paint borders
            // Step 3: Initialize current pointer location
            // Step 4: Initialize previous pointer location

            // THIS LESSON:

            // Step 5: Initialize image data
            // Step 6: Main loop (adapted!)

            // PREVIOUS LESSON:

            // Step 7: Say goodbye
        }
    }
}
```

In the following, we first explain how to set up the data structure for our image before we dive into the necessary changes to the main loop.

## Step 5: Initialize image data

To store our drawing, we need a place in memory. We'll use a one-dimensional array, where each element represents the color of a single pixel on our canvas. But first, we need to figure out how big our canvas is. The drawing below illustrates that our drawing area (the "image") is the size of the console window minus the borders on each side.

![Blackboard drawing visualizing the difference between console window width/height, top/bottom border rows, left/right border columns, remaining image width/height, and computation of pixel count from image width and height](./Console%20Window%20and%20Image%20Data.jpg)

Since the border takes up one character on the left and one on the right, the width of our image is the total window width minus two. The same logic applies to the height.

```csharp
int imageWidth = Console.WindowWidth - 2;
int imageHeight = Console.WindowHeight - 2;
```

With the width and height of our image, we can calculate the total number of pixels it contains by multiplying them.

```csharp
int imageSize = imageWidth * imageHeight;
```

Now we can create the array that will hold our image data. We'll create an array of `ConsoleColor` with the size we just calculated. This array, which we'll call `imageData`, will store the color of every pixel in our drawing area.

```csharp
ConsoleColor[] imageData = new ConsoleColor[imageSize];
```

Before we can use this array, we should give every pixel an initial color. A `for` loop is perfect for this. We'll iterate through every element of the `imageData` array and set its color to `ConsoleColor.Black`. This gives us a black canvas to start with.

```csharp
for (int pixel = 0; pixel < imageSize; pixel++) {
    imageData[pixel] = ConsoleColor.Black;
}
```

There's one more important detail. The console window's coordinates start at (0, 0) in the top-left corner. However, our drawing area is offset from the edge because of the border. The drawing below shows this offset. Our image's top-left corner is at the console coordinate (1, 1). We'll store this offset in variables to make it easy to convert between image coordinates and console coordinates.

![Blackboard drawing introducing the image offset vector](./Console%20Window%20and%20Image%20Coordinates%202.jpg)

These variables will hold the offset of our image relative to the console window.

```csharp
int imageOffsetX = 1;
int imageOffsetY = 1;
```

## Step 6: Adapted main loop

Our main loop will still handle moving the pointer, but we need to adapt it to work with our new `imageData` array. The core logic remains the same, but how we clear and paint the pointer will change. We also need to add the logic for changing a pixel's color.

```csharp
while (true)
{
    // THIS LESSON:

    // Step 6.1: Clear previous pointer location (adapted!)
    // Step 6.2: Paint current pointer location (adapted!)

    // PREVIOUS LESSON:

    // Step 6.3: Update previous pointer location

    // THIS LESSON:

    // Step 6.4: Read and process next user input (adapted!)
}
```

Let's look at the adapted steps in detail.

### Step 6.1: Clear previous pointer location

Previously, we erased the old pointer by just writing a space character, which used the default black background. Now, we need to be smarter. When the pointer moves away, we must restore the original color of the pixel that was underneath it. This color is stored in our `imageData` array. The drawing below explains how we can find the correct color in our one-dimensional `imageData` array using the two-dimensional (X, Y) coordinates of the pointer. The formula is `index = Y * imageWidth + X`.

![Blackboard drawing explaining the computation of pixel indices in the image data array from pixel x/y coordinates and image width](./Image%20Data%20Array%20Indexing.jpg)

We use this formula to get the color from the `imageData` array at the pointer's previous location and set it as the `Console.BackgroundColor`.

```csharp
Console.BackgroundColor = imageData[previousY * imageWidth + previousX];
Console.ForegroundColor = ConsoleColor.White;
```

Now that the background color is set correctly, we need to write the space character at the right position on the screen. Remember our offset: the pointer's coordinates are relative to the image, not the console window. The drawing below illustrates how we convert from image coordinates to window coordinates by adding the offset.

![Blackboard drawing explaing the computation of image coordinates from window coordinates minus offset vector, and window coordinates from image coordinates plus offset vector](./Console%20Window%20and%20Image%20Coordinates%203.jpg)

We add the `imageOffsetX` and `imageOffsetY` to the pointer's `previousX` and `previousY` coordinates to get the correct position in the console window before writing the space character.

```csharp
Console.SetCursorPosition(imageOffsetX + previousX, imageOffsetY + previousY);
Console.Write(' ');
```

### Step 6.2: Paint current pointer location

Painting the pointer at its new location follows the exact same logic. We first look up the color of the pixel at the new location in our `imageData` array and set it as the background color. This ensures that if we move our pointer onto a colored pixel, the background of our 'X' character will have the correct color.

```csharp
Console.BackgroundColor = imageData[currentY * imageWidth + currentX];
Console.ForegroundColor = ConsoleColor.White;
```

Then, we move the cursor to the correct screen position by adding the offset to the current coordinates and draw our 'X' character.

```csharp
Console.SetCursorPosition(imageOffsetX + currentX, imageOffsetY + currentY);
Console.Write('X');
```

### Step 6.4: Read and process next user input

To allow the user to paint, we need to react to a new key: the spacebar. We still read the key with `Console.ReadKey(true)` to keep the screen clean.

```csharp
ConsoleKeyInfo input = Console.ReadKey(true);
```

Our `if-else if` block is now extended with a new case. If the user presses the spacebar, we will trigger our new "update pixel color" logic.

```csharp
if (input.Key == ConsoleKey.UpArrow)
    // Case a: Move pointer up
else if (input.Key == ConsoleKey.DownArrow)
    // Case b: Move pointer down
else if (input.Key == ConsoleKey.LeftArrow)
    // Case c: Move pointer left
else if (input.Key == ConsoleKey.RightArrow)
    // Case d: Move pointer right
else if (input.Key == ConsoleKey.Spacebar)
    // Case e: Update pixel color (NEW!!!)
else if (input.Key == ConsoleKey.Escape)
    // Case f: Break main loop
else
    // Case g: Ignore input
```

Let's look at the new case.

#### Case e: Update pixel color

This is where we finally change the drawing! When the user presses the spacebar, we want to change the color of the pixel under the pointer. We use our indexing formula (`currentY * imageWidth + currentX`) to find the right spot in the `imageData` array and set its value to `ConsoleColor.Red`.

```csharp
imageData[currentY * imageWidth + currentX] = ConsoleColor.Red;
```

And that's it! Now, when the main loop runs again, the `Paint current pointer location` step will read this new red color and our 'X' will be drawn with a red background. If we move the pointer away, the `Clear previous pointer location` step will also read this red color and fill the spot with a red space, making the pixel permanently red.