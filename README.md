# Xamarin.Droid.UIToolbox

## Why use this project?

Xamarin.Droid.UITooolbox (formely toolboox4droid) is Android class library used to create quickly single activity application with multiple fragment. It provides API for push/pop navigation, drawer (horizontal) navigation, modal (bottom) navigation. 
It also provide skeletoons for common screen implementation such as displaying a list of item or creating an input form.
As of version 2.0.0, this project depends 100% on AndroidX libraries.

## How to install

This project is available as a nugget package: https://www.nuget.org/packages/toolbox4droid.f10.net/2.0.0

## features

### Fragment based navigation

Following the [Fragment Navigation Pattern](https://www.toptal.com/android/android-fragment-navigation-pattern), the recommandation are as follow :
- Fragment should focus on the part of app that they represent, and not be aware of where / how / when they are displayed. Hence Fragment should not know when trigger a navigation.
- Activities should handle display and navigation of fragment.

It doesn't necessecary means to have only one Activity per project, but rather, one activity for a bundle of fragment related to each other. For example, you can have an activity dedicated to login, that will handle the navigation between the LoginFragment, SignupFragment, ForgottenPasswordFragment...
Each Fragment will be responsible to manage the view it contains, and when an action is triggered, the activity will be notified and, decide wich navigation to perform.

In this package, you will find the PushPopActivity, an abstract class you can adapt to your own need. Select the class of the Fragment you want to display as the root of your app, and use Push(), Pop() method to triger horizontal navigation. Vertical navigation can be obtain by using `PresentModally()`.

You need a hamburger Menu Navigation ? Thant can be achieve by overriding the appropriate method.

example : 

