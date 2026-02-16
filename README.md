# Xamarin.Droid.UIToolbox

## Why use this project?

Xamarin.Droid.UIToolbox (formerly toolbox4droid) is an Android class library for .NET 9 designed to quickly create single-activity applications with multiple fragments. It provides a robust API for:
- Stack-based navigation (Push/Pop)
- Drawer (hamburger menu) navigation
- Modal navigation
- Efficient list implementations
- Quick input form generation

As of version 2.0.0, this project is fully migrated to **.NET 9** and depends 100% on **AndroidX** libraries.

## How to install

This project is available as a NuGet package:
[https://www.nuget.org/packages/toolbox4droid.f10.net/](https://www.nuget.org/packages/toolbox4droid.f10.net/)

```bash
dotnet add package toolbox4droid.f10.net
```

## Features

### Fragment-based Navigation

Following the [Fragment Navigation Pattern](https://www.toptal.com/android/android-fragment-navigation-pattern):
- **Fragments** focus on their own representation and data, remaining unaware of how or when they are displayed.
- **Activities** handle the orchestration and navigation between fragments.

#### PushPopActivity

`PushPopActivity` is an abstract class that manages a stack of fragments.

```csharp
[Activity(Label = "My App", MainLauncher = true)]
public class MainActivity : PushPopActivity
{
    // Define the first fragment to be displayed
    protected override Type GetRootFragmentClass => typeof(HomeFragment);
}
```

In your fragments, you can trigger navigation using:
```csharp
// Push a new fragment onto the stack
((PushPopActivity)Activity).Push<DetailFragment>();

// Go back
Back(); // or Activity.OnBackPressed()
```

#### DrawerActivity

`DrawerActivity` adds support for a navigation drawer (hamburger menu). Override `OnPrepareSideMenu` to define your menu items.

---

### Displaying Lists with SmartListFragment

`SmartListFragment<T>` simplifies implementing `ListView` with a `SwipeRefreshLayout`.

```csharp
public class MyListFragment : SmartListFragment<MyModel>
{
    public override IList<MyModel> Elements => MyViewModel.Items;

    public override int GetCellResource(int position) => Resource.Layout.my_item_cell;

    public override void RegisterCell(View cell, ViewHolder vh)
    {
        vh.Views["Title"] = cell.FindViewById<TextView>(Resource.Id.titleText);
    }

    public override void Bind(ViewHolder vh, MyModel item)
    {
        vh.getView<TextView>("Title").Text = item.Name;
    }

    protected override void OnItemSelected(object sender, MyModel item)
    {
        // Handle click
    }
}
```

---

### Input Forms with FormFragment

`FormFragment` allows you to build data entry screens quickly using various pre-built cells.

```csharp
public class MyFormFragment : FormFragment
{
    protected override void ConfigureForm()
    {
        InsertHeader("User Profile");
        
        AddRow(new InputCell(TAG_NAME, this, "Name", currentName, InputTypes.ClassText));
        AddRow(new SwitchCell(TAG_ADMIN, this, "Is Admin", isAdmin));
        AddRow(new DatePickerCell(TAG_BIRTH, this, "Birth Date", birthDate));
        
        InsertFooter("Please ensure all data is correct.");
    }

    public override void OnCellDataChanged(Cell cell, object newValue)
    {
        // Handle data changes based on cell.Tag
    }
}
```

Available Cells: `InputCell`, `DatePickerCell`, `TimePickerCell`, `SwitchCell`, `ListPickerCell`, `ButtonCell`, etc.

---

### HUD (Heads-Up Display)

The library includes a simple HUD for showing loading states or progress.

```csharp
HUD.Loader.Show(Context, "Loading...");
// ... perform async task ...
HUD.Loader.Hide();
```

---

## Maintenance & Publishing

To publish a new version of the package:

1. Update the version and dependencies in `toolbox4droid.nuspec`.
2. Build the solution in Release mode:
   ```bash
   dotnet build -c Release
   ```
3. Pack and upload:
   ```bash
   nuget pack toolbox4droid.nuspec
   nuget push *.nupkg -Source https://api.nuget.org/v3/index.json
   rm -rf *.nupkg
   ```

### Using Fastlane

You can also use Fastlane to automate the build and publish process.

1. Install Fastlane:
   ```bash
   bundle install
   ```

2. Run the publish lane:
   ```bash
   bundle exec fastlane publish
   ```
   *IMPORTANT: You **must** set the `NUGET_API_KEY` environment variable to authenticate with NuGet.org:*
   ```bash
   export NUGET_API_KEY=your_api_key_here
   bundle exec fastlane publish
   ```

