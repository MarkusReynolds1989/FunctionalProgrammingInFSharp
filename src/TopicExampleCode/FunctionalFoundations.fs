namespace TopicExampleCode

module FunctionalFoundations =
    // A class is great when we want to bundle functionality around
    // mutable values. We can encapsulate the mutable fields
    // and prevent anyone from accessing them in a way that
    // we don't intend.
    
    // This has the draw back of adding extra complexity where we
    // can mutate values without understanding the full context of their mutation.
    type Person (name, age) =
        let mutable age = age
        member this.name = name
        
        member this.addAge = age <- age + 1
        member this.getAge = age
    
    let personCreation =
        let tom = Person("tom", 23)
        tom.addAge
        tom.addAge
        tom.getAge 