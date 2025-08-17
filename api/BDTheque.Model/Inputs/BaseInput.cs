namespace BDTheque.Model.Inputs;

[ApplyMutationInputMapping]
public abstract class BaseInput;

public abstract class BaseCreateInput : BaseInput;

public abstract class BaseUpdateInput : BaseInput;

#pragma warning disable S2094 // needed as class
public abstract class BaseNestedInput;
#pragma warning restore S2094
