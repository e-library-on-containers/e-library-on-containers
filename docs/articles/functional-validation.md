# FunctionalValidaton library

FunctionalValidaton is a validation library utilizing functional concepts and CSharpFunctionalExtensions library, mainly *Either<TPayload, TError>*, which in this library is *Result<TPayload, TError>*. 

*AbstractValidator<TType>* implementaions define validation rules that *TType* has to pass to be valid. Should any validation fail, *Result<TPayload, TError>* will be returned as failure, containing *ApplicationError* with its properties. In case of passing all rules, it will be returned as success and contain validated subject.

To register all validators, FunctionalValidaton uses Assembly scanning (hence Assembly is passed as parameter in *AddFunctionalValidation* extensions method) to find all validators and register them in IoC container.

As this is a shared library, all exposed classes are thoroughly documented and described for easier utilization of this library.