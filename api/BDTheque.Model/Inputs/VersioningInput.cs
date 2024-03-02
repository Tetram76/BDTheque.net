namespace BDTheque.Model.Inputs;

[ApplyMutationInputMapping]
public abstract class VersioningInput;

public abstract class VersioningCreateInput : VersioningInput;

public abstract class VersioningUpdateInput : VersioningInput;

#pragma warning disable S2094
public abstract class VersioningNestedInput;
#pragma warning restore S2094
