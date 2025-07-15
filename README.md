# TerminalPaint

**TerminalPaint** is a basic paint application running in the terminal.
The application features a color picker, rectangle and line drawing tools, as well as loading and saving of images.
The color picker provides access to a small palette of colors supported on a wide range of platforms (e.g. Windows, Linux, and MacOS).
The line drawing tool uses a simple implementation of the Bresenham algorithm for line discretization.
And image loading and saving uses a custom image format, which intentionally is not compatible with other tools.
The application itself is written in C# with .NET Core.

We use this application for teaching programming at the [School of Engineering](https://fh-ooe.at/en/campus-wels) of the [University of Applied Sciences Upper Austria](https://fh-ooe.at/en).
According to our experience, the application is simple enough to be fully unterstood even by beginners, but interesting enough to motivate students to work on the assigments.

*Enjoy! 😉*

## Screencasts

Here are some screencasts showing **TerminalPaint** in action:

![](./Screencasts/Rectangles%20and%20Fill.gif)

## Lessons

We use the **TerminalPaint** application to teach the following lessons:

- 📖 [**Lesson 1: Console basics**](./Lessons/01_Console.md) - Working with the *C# Console API*.
- 📖 [**Lesson 2: Image representation**](./Lessons/02_Image.md) - Representing images in computer memory.
- 📖 [**Lesson 3: Color selection**](./Lessons/03_Color.md) - Implementing a color picking feature.
- 📖 [**Lesson 4: Clear operation**](./Lessons/04_Clear.md) - Clearing the entire image.
- 📖 [**Lesson 5: Store operation**](./Lessons/05_Store.md) - Storing images to and loading images from disk.
- 📖 [**Lesson 6: Fill operation**](./Lessons/06_Fill.md.md) - Filling image regions with a new color.
- 📖 [**Lesson 7: Rectangle operation**](./Lessons/07_Rectangle.md) - Implementing a rectangle drawing tool.
- 📖 [**Lesson 8: Line operation**](./Lessons/08_Line.md.md) - Implementing a line drawing tool.
- 📖 [**Lesson 9: Outlook**](./Lessons/09_Outlook.md) - Where you can go from here.

## Drawings

Here are some drawings made with **TerminalPaint**:

<img src="./Screenshots/Flower%20Heart%20with%20Textbars.png" width="50%"/><img src="./Screenshots/Rocket%20with%20Color%20Picker.png" width="50%"/>
<img src="./Screenshots/Rectangles%20and%20Lines.png" width="50%"/><img src="./Screenshots/Man%20with%20Hat.png" width="50%"/>

## Documents

Finally, here are the standard documents shipped with open source software:

* 📄 [**LICENSE.md**](./LICENSE.md) - Explains the license of the source code.
* 📄 [**CHANGELOG.md**](./CHANGELOG.md) - Summarizes major changes to the source code.
* 📄 [**CONTRIBUTING.md**](./CONTRIBUTING.md) - Defines guidelines for contributing to the source code. 