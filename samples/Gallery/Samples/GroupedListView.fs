namespace Gallery.Samples

open System.Collections.ObjectModel
open Fabulous.Maui
open Gallery

open type Fabulous.Maui.View

module GroupedListView =

    type Contact = { FirstName: string; LastName: string }

    type ContactGroup(name: string, contacts: Contact list) =
        inherit ObservableCollection<Contact>(contacts)
        member _.ShortName = name.[0]
        member _.Name = name

    type Model =
        { Contacts: Contact list
          ContactGroups: ObservableCollection<ContactGroup> }

    type Msg = Id

    let init () =
        { Contacts = []
          ContactGroups =
            ObservableCollection<ContactGroup>(
                [ for i in 0..3 do
                      ContactGroup(
                          $"Header {i}",
                          [ { FirstName = "John"; LastName = "Doe" }
                            { FirstName = "Jane"; LastName = "Doe" }
                            { FirstName = "John"; LastName = "Smith" } ]
                      ) ]
            ) }

    let update msg model =
        match msg with
        | Id -> model

    let view model =
        (GroupedListView model.ContactGroups (fun group -> TextCell group.Name) (fun contact -> TextCell $"{contact.FirstName} {contact.LastName}"))
            .rowHeight(60)
            .fillVertical()

    let sample =
        { Name = "GroupedListView"
          Description = "A ListView with sections"
          Program = Helper.createProgram init update view }
