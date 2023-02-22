namespace Gallery.Samples

open System.Collections.ObjectModel
open Fabulous.Maui
open Gallery
open Microsoft.Maui.Controls

open type Fabulous.Maui.View

module GroupedCollectionView =

    type Contact = { FirstName: string; LastName: string }

    type ContactGroup(header: string, contacts: Contact list, footer: string) =
        inherit ObservableCollection<Contact>(contacts)
        member _.Header = header
        member _.Footer = footer

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
                            { FirstName = "Pepe"; LastName = "Doe" }
                            { FirstName = "Julia"
                              LastName = "Smith" } ],
                          $"Footer {i}"
                      ) ]
            ) }

    let update msg model =
        match msg with
        | Id -> model

    let view model =
        (GroupedCollectionView
            model.ContactGroups
            (fun group -> Label(group.Header).font(attributes = FontAttributes.Bold))
            (fun contact -> Label($"{contact.FirstName} {contact.LastName}"))
            (fun group -> Label($"{group.Footer}").font(attributes = FontAttributes.Bold)))
            .selectionMode(SelectionMode.Multiple)
            .itemSizingStrategy(ItemSizingStrategy.MeasureAllItems)

    let sample =
        { Name = "GroupedCollectionView"
          Description = "A collection view that groups items"
          Program = Helper.createProgram init update view }
