using System;
using Xunit;

namespace xUnitTest
{
    public class when_an_empty_string_is_provided : with_a_StringCalculator
    {
        public override void Given()
        {
            base.Given();
            input = string.Empty;
        }

        [Fact]
        public void it_should_return_zero()
        {
            Assert.Equal(0, result);
        }
    }

    public class when_a_single_number_is_provided : with_a_StringCalculator
    {
        public override void Given()
        {
            base.Given();
            input = "1";
        }

        [Fact]
        public void it_should_return_the_provided_number()
        {
            Assert.Equal(1, result);
        }
    }

    public class when_two_numbers_are_provided : with_a_StringCalculator
    {
        public override void Given()
        {
            base.Given();
            input = "1,2";
        }

        [Fact]
        public void it_should_return_the_sum_of_the_two()
        {
            Assert.Equal(3, result);
        }
    }

    public class when_the_input_is_multiple_numbers : with_a_StringCalculator
    {
        public override void Given()
        {
            base.Given();
            input = "1,2,4";
        }

        [Fact]
        public void it_should_return_the_of_all_the_numbers()
        {
            Assert.Equal(7, result);
        }
    }

    public class when_the_numbers_delimiter_is_newLine : with_a_StringCalculator
    {
        public override void Given()
        {
            base.Given();
            input = "1\n2\n4";
        }

        [Fact]
        public void it_should_return_the_of_all_the_numbers()
        {
            Assert.Equal(7, result);
        }
    }

    public class when_there_are_consecutive_delimiters : with_a_StringCalculator
    {
        Exception exception;
        public override void Given()
        {
            base.Given();
            input = ",,";
        }

        public override void When()
        {
            try
            {
                base.When();
            }
            catch (Exception ex)
            {
                exception = ex;
            }
        }
        [Fact]
        public void it_should_throw_exception()
        {
            Assert.Equal(typeof(ArgumentException), exception.GetType());
        }
    }

    public class when_a_custom_delimiter_is_provided : with_a_StringCalculator
    {
        public override void Given()
        {
            base.Given();
            input = "\\;\n,1,2";
        }

        [Fact]
        public void it_should_return_the_sum_of_all_numbers()
        {
            Assert.Equal(3, result);
        }
    }

    public class when_a_negative_number_is_provided : with_a_StringCalculator
    {
        Exception exception;
        public override void Given()
        {
            base.Given();
            input = "-1,2";
        }

        public override void When()
        {
            try
            {
                base.When();
            }
            catch (Exception ex)
            {
                exception = ex;
            }
        }

        [Fact]
        public void it_should_throw_exception()
        {
            Assert.Equal(typeof(ArgumentOutOfRangeException), exception.GetType());
        }

        [Fact]
        public void it_should_provide_an_error_message_that_contains_the_negative_number()
        {
            Assert.True(exception.Message.Contains("-1"));
        }
    }

    public abstract class with_a_StringCalculator : SpecificationContext
    {
        protected StringCalculator sut;
        protected string input;
        protected int result;
        public with_a_StringCalculator()
        {
            this.Given();
            this.When();
        }

        public override void Given()
        {
            sut = new StringCalculator();
        }

        public override void When()
        {
            result = sut.Add(input);
        }
    }

    public abstract class SpecificationContext
    {
        public virtual void Given() { }
        public virtual void When() { }
    }
}