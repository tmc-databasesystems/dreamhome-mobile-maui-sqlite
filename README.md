
# DreamHome Case Study - Mobile App developed in MAUI using LINQ and Entity Framework with a local SQLite database

This repository demonstrates the use of Entity Framework Core and LINQ in a mobile app developed using MAUI based on the DreamHome case study, using a local SQLite database.


## Main Classes

The main classes in this project are as follows:

- **DreamHomeDbContext**: the Entity Framework Core database context for the application. It exposes a `DbSet` for each of the base tables in the DreamHome case study and uses the `OnModelCreating` method to configure the mapping between the entity classes and the database schema, including tables, keys, and relationships.

- **DreamHomeService**: contains the application logic used by the app. Its methods are called by the page behind C# classes and handle any processing required before or after interacting with the database.

- **DreamHomeRepository**: is responsible for data access. It contains the LINQ queries and Entity Framework Core operations used to retrieve, insert, and update data in the DreamHome database. In the .NET MAUI application, the repository uses `IDbContextFactory<DreamHomeDbContext>` rather than holding a single long-lived `DreamHomeDbContext`. This factory creates short-lived `DbContext` instances on demand, which is a good fit for MAUI because mobile apps do not have ASP.NET Core’s per-request lifetime model, and it is generally preferable to create and dispose contexts per unit of work rather than keep one context alive across pages and UI interactions. Because a fresh `DbContext` is typically created for each database operation, entity tracking does not carry over from one operation to the next. As a result, tracking is mainly useful only within a single unit of work, where the same `DbContext` instance is used to retrieve, modify, and save entities. For simple read-only queries, no-tracking queries are often appropriate. :contentReference[oaicite:0]{index=0}

- **Program.cs**: configures dependency injection for the main application components so that the required services, repositories, and database context are provided automatically at runtime.

## Dependency Structure

The application follows a layered structure:

- `DreamHomeService` depends on `DreamHomeRepository`
- `DreamHomeRepository` depends on `IDbContextFactory<DreamHomeDbContext>`

## How to run the project


### Running the project in Visual Studio

Before running the application in Visual Studio, first check that the MAUI project is set up to build and deploy for the active configuration. Open **Build > Configuration Manager** and confirm that the project is included in the active solution configuration, with **Build** enabled and **Deploy** enabled if deployment is required for your setup. Visual Studio’s Configuration Manager is the place where you select the active solution configuration and choose whether each project should be built and deployed. :contentReference[oaicite:1]{index=1}

Next, set up an Android emulator. In Visual Studio, open **Tools > Android > Android Device Manager** and create an Android Virtual Device (AVD) if you do not already have one. The Android Device Manager is used to create and configure Android virtual devices for testing .NET MAUI apps. Once the emulator has been created, start it and allow it to boot fully. :contentReference[oaicite:2]{index=2}

After the emulator is running, select it in the Visual Studio toolbar as the debug target. Microsoft’s .NET MAUI guidance shows choosing an Android emulator from the debug target drop-down and then running the app from Visual Studio. In many cases Visual Studio can also install the default Android SDK and emulator components when required. :contentReference[oaicite:3]{index=3}

Once the emulator has been selected, run the MAUI application in the normal way by pressing **F5** or clicking the **Run** button in Visual Studio. The app will then be built, deployed to the selected emulator, and started there. :contentReference[oaicite:4]{index=4}


### Running the project in Visual Studio Code

To run a .NET MAUI project in Visual Studio Code, you should first ensure that Visual Studio Code has the **.NET MAUI extension** installed, together with the C# tooling it depends on. Microsoft provides a dedicated .NET MAUI extension for VS Code, and the official .NET MAUI getting-started guidance for VS Code also lists the .NET SDK, MAUI SDK, Android SDK, and an Android emulator as prerequisites when targeting Android. :contentReference[oaicite:5]{index=5}

Open the project folder in Visual Studio Code. Then use the command palette and run **.NET MAUI: Configure Android**, followed by **Refresh Android environment**, to verify that the Android toolchain is correctly configured. Any reported errors should be fixed before trying to run the app. :contentReference[oaicite:6]{index=6}

Next, ensure that an Android emulator is available and running. You can create and manage Android Virtual Devices using the Android tooling, and the MAUI documentation describes these virtual devices as the standard way to run and test Android apps without a physical device. :contentReference[oaicite:7]{index=7}

Once the emulator is running, select it as the debug target in Visual Studio Code. Microsoft’s MAUI guidance says this can be done from the status bar by selecting the debug target, or from the command palette with **.NET MAUI: Pick Android Device**. :contentReference[oaicite:8]{index=8}

Finally, run the project by pressing **F5** or using the **Run** button in the top-right corner of Visual Studio Code. This builds the MAUI application and launches it on the selected Android emulator.


When you run the project, the first page displays a list of branches. When you select a branch, you will be taken to a page that displays the properties for the selected bramch. If you use the tab bar at the botton you can select Staff and you will get a list of staff for the selected branch. From this page, you can click the Add Staff button to add a new member of staff or you can click on a member of staff in the table to update the details for that member of staff. In the tab bar, there is also a Queries options which includes the LINQ query examples discussed in Section 27.1.5 of the book:

- **Example 27.1 - LINQ comparison search condition**: lists the staff whose salary is greater than a specified threshold.
- **Example 27.2 - LINQ use of aggregate**: returns the number of different properties viewed between a given start date and end date. Enter dates in the format `yyyy-MM-dd`.
- **Example 27.3 - LINQ simple join**: lists the names of all clients who have viewed a property, together with any comments they supplied.
- **Example 27.4 - LINQ multiple grouping columns**: returns the number of properties handled by each member of staff, together with the branch number of that member of staff.
- **Example 27.5 - LINQ use of Any**: finds all staff who work in a branch office in a specified city.