Описание предметной области
---
Автоматизация создания договоров для фотостудии.

Автор
---
Бажин Кирилл Андреевич студент группы ИП 20-3Схема моделей
---
```mermaid
    classDiagram
    Dogovor <.. Photograph
    Dogovor <.. PhotoSet
    Dogovor <.. Client
    Dogovor <.. Product
    Dogovor <.. Recvisit
    Dogovor <.. Uslugi
    BaseAuditEntity --|> Uslugi
    BaseAuditEntity --|> Recvisit
    BaseAuditEntity --|> Client
    BaseAuditEntity --|> Product
    BaseAuditEntity --|> Photograph
    BaseAuditEntity --|> PhotoSet
    BaseAuditEntity --|> Dogovor
    IEntity ..|> BaseAuditEntity
    IEntityAuditCreated ..|> BaseAuditEntity
    IEntityAuditDeleted ..|> BaseAuditEntity
    IEntityAuditUpdated ..|> BaseAuditEntity
    IEntityWithId ..|> BaseAuditEntity
    class IEntity{
        <<interface>>
    }
    class IEntityAuditCreated{
        <<interface>>
        +DateTimeOffset CreatedAt
        +string CreatedBy
    }
    class IEntityAuditDeleted{
        <<interface>>
        +DateTimeOffset? DeletedAt
    }
    class IEntityAuditUpdated{
        <<interface>>
        +DateTimeOffset UpdatedAt
        +string UpdatedBy
    }
    class IEntityWithId{
        <<interface>>
        +Guid Id
    }
class Product{
         +string Name
        +string Price
        +string? Amount
    }      
    class PhotoSet{
         +string Name
        +string Description
        +int Price
    }
    class Photograph {
        +string LastName
        +string Name
        +string? Number
    }
    class Client {
        +string LastName
        +string Name
        +string? Number
    }
    class Recvisit {
        +string Name
        +string? Description
        +int Amount
    }

    class Uslugi {
        +string Name
        +string Price
    }
    class Dogovor {
        +Guid Photographd 
        +Guid PhotoSetId
        +Guid ProductId
        +Guid ClientId
        +Guid RecvisitId
        +Guid UslugitId
        +decimal Price
        +DateTimeOffset Date
    }
    class Post {
        <<enumeration>>
        Cashier(Кассир)
        Manager(Менеджер)
    }
    class BaseAuditEntity {
        <<Abstract>>        
    }
```
