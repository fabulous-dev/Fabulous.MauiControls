namespace Fabulous.MauiControls.Tests

open System
open Fabulous
open NUnit.Framework

open Fabulous.Maui

open type Fabulous.Maui.View

[<TestFixture>]
type Tests() =
    [<Test>]
    member _.Test() =
        let dispatchedMsgs = ResizeArray<string>()
        let dispatch msg = dispatchedMsgs.Add(unbox<string> msg)

        let oldWidget =
            NavigationPage() {
                ContentPage("Onboarding", Label("Hello")).automationId("onboarding")

                ContentPage("HowMuch", Label("Hello")).automationId("howMuch")

                ContentPage("RepaymentDate", Label("Hello")).automationId("repaymentDate")

                ContentPage("YourDetails", Label("Hello")).automationId("yourDetails")
            }

        let newWidget =
            NavigationPage() {
                ContentPage("Onboarding", Label("Hello")).automationId("onboarding")

                ContentPage("HowMuch", Label("Hello")).automationId("howMuch")

                ContentPage("RepaymentDate", Label("Hello")).automationId("repaymentDate")

                ContentPage("VerificationCode", Label("Hello"))
                    .automationId("verificationCode")
                    .onMounted("verificationCodeMounted")
                    .onUnmounted("verificationCodeUnmounted")
            }

        let newWidget2 =
            NavigationPage() {
                ContentPage("Onboarding", Label("Hello")).automationId("onboarding")

                ContentPage("HowMuch", Label("Hello")).automationId("howMuch")

                ContentPage("RepaymentDate", Label("Hello")).automationId("repaymentDate")

                ContentPage("YourAddress", Label("Hello")).automationId("yourAddress")
            }

        let treeContext: ViewTreeContext =
            { CanReuseView = XViewHelpers.canReuseView
              GetViewNode = ViewNode.get
              Logger = XViewHelpers.defaultLogger()
              Dispatch = dispatch }

        let navPage = CustomNavigationPage()
        let weakRef = WeakReference(navPage)

        let node = ViewNode(None, treeContext, weakRef)

        Reconciler.update treeContext.CanReuseView ValueNone (oldWidget.Compile()) node
        Reconciler.update treeContext.CanReuseView (ValueSome(oldWidget.Compile())) (newWidget.Compile()) node
        Reconciler.update treeContext.CanReuseView (ValueSome(newWidget.Compile())) (newWidget2.Compile()) node

        Assert.AreEqual("yourAddress", navPage.PagesSync[3].AutomationId)
        Assert.AreEqual(7, dispatchedMsgs.Count)
