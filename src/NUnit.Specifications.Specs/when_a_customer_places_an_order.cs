using NUnit.Specifications;
using Should;

public class OrderSpecs
{
	[Component]
	public class when_a_customer_places_an_order : ContextSpecification
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

		It should_successfully_place_the_order = () => _results.ShouldBeTrue();
	}
}




class Order
{
}


public class OrderService
{
	public bool PlaceOrder(Order order)
	{
		return true;
	}
}