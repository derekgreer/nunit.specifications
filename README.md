# NUnit.Specifications

NUnit.Specifications is a context/specification library for use with the NUnit testing framework.

# Quickstart

The following is an example NUnit test written using the ContextSpecification base class from NUnit.Specifications:

```C#
using NUnit.Framework;
using NUnit.Specifications;

public class OrderSpecs
{
	[Subject("Order Processing")]
	public class when_a_customer_places_an_order: ContextSpecification
	{
		static OrderService _orderService;
		static bool _results;
		static Order _order;
		
		Establish context = () =>
		{
			_orderService = new OrderService();
			_order = new Order();
		};
		
		Because of = () => _results = _orderService.PlaceOrder(_order);

		It should_successfully_place_the_order = () => Assert.IsTrue(_results);
	}
}
```

See the [NUnit.Specifications Wiki](https://github.com/derekgreer/nunit.specifications/wiki) for more information and examples.
