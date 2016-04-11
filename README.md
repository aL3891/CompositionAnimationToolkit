# CompositionAnimationToolkit

The CompositionAnimationToolkit is a set of tools that is ment to make working with animations in the Windows.Ui.Composition libraries easier. Specifically, it allows users to write animation expressions
using statically typed lambda expressions instead of using strings. It also has features to make working with property sets easier, such as providing a wrapper that user classes can derive from to make static properties
for the contents in the property set, as well as greating property sets from any other class, including anonymous classes.

## Lambda expressions
Typically expressions are written as strings, and any symbols used in the expressions will have to be added later. Using the extension methods in this library a c# lambda can be used to devine the expression instead.
The lambda can close over variables in the surronding context and the library will automatically add them to the expression.

A typical expression can look like this:

    CompositionPropertySet propertySet = compositor.CreatePropertySet();
    propertySet.InsertScalar("Rotation", 0f);
    propertySet.InsertVector3("CenterPointOffset", new Vector3(redSprite.Size.X / 2 - blueSprite.Size.X / 2,
        redSprite.Size.Y / 2 - blueSprite.Size.Y / 2, 0));

    expressionAnimation.Expression = "visual.Offset + " +
        "propertySet.CenterPointOffset + " +
        "Vector3(cos(ToRadians(propertySet.Rotation)) * 150," +
        "sin(ToRadians(propertySet.Rotation)) * 75, 0)";

    expressionAnimation.SetReferenceParameter("propertySet", propertySet);
    expressionAnimation.SetReferenceParameter("visual", redSprite);

Using the a lambda expression this can instead be written as

    expressionAnimation.ExpressionLambda(c => redSprite.Offset + propertySet.Get<Vector3>("CenterPointOffset")
        + c.Vector3(c.Cos(c.ToRadians(propertySet.Get<float>("Rotation"))) * 150, c.Sin(c.ToRadians(propertySet.Get<float>("Rotation"))) * 75, 0));

And the SetReferenceParameter calls can be left out

Additionally, the property set can be defined as anonymous class instead:

    var propertySet = new
    {
        Rotation = 0,
        CenterPointOffset = new Vector3(redSprite.Size.X / 2 - blueSprite.Size.X / 2, redSprite.Size.Y / 2 - blueSprite.Size.Y / 2, 0)
    };

The expression can then be written as:

    expressionAnimation.ExpressionLambda(c => redSprite.Offset + propertySet.CenterPointOffset
        + c.Vector3(c.Cos(c.ToRadians(propertySet.Rotation)) * 150, c.Sin(c.ToRadians(propertySet.Rotation)) * 75, 0));

To be able to pass around property sets, there is also a class called CompositionPropertySetWrapper that can be derived from. An example of such a class can be:

    public class MyPropertySet : CompositionPropertySetWrapper
    {
        public PropertySet(Compositor comp) : base(comp) { }
        public float Rotation { get { return GetScalar(); } set { SetValue(value); } }
        public Vector3 CenterPointOffset { get { return GetVector3(); } set { SetValue(value); } }
    }

It could then be used as above. The system will either use the wrapped property set or make a new property set and use that in the animation. 
The ExpressionLambda will also return a dictionaty with all the parameters used in the expression so they can be used to start animations for example:

    var props = expressionAnimation.ExpressionLambda(...);
    ((CompositionPropertySet)props["propertySet"]).StartAnimation("Rotation", rotAnimation);

## Future work
This library has partially been validated against the samples in https://github.com/Microsoft/WindowsUIDevLabs (in this branch https://github.com/aL3891/WindowsUIDevLabs) but that work is not complete.
in addition i'd like to look at if story boards can perhaps be converted to composition animation. Another idea is to implement currently unsupported functions like dot product as expanded expressions.

If you have any ideas for things you'd like to see, or any other feedback, please do create an issue!

