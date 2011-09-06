using System;
using AutoMapper;

namespace NBTY.Core
{
    public static class MappingObjectExtensions
    {
        public static TOutput Map<TOutput>(this object input)
        {
            return (TOutput)input.Map(typeof(TOutput));
        }

        public static TOutput Map<TOutput>(this object input, TOutput output)
        {
            try
            {
                Mapper.Map(input, output, input.GetType(), typeof(TOutput));   
            }
            catch (AutoMapperMappingException)
            {
                Mapper.DynamicMap(input, output, input.GetType(), typeof(TOutput));
            }
            
            return output;
        }

        public static object Map(this object input, object output, Type outputType)
        {
            try
            {
                Mapper.Map(input, output, input.GetType(), outputType);
            }
            catch (AutoMapperMappingException)
            {
                Mapper.DynamicMap(input, output, input.GetType(), outputType);
            }

            return output;
        }

        public static object Map(this object input, Type outputType)
        {
            if (outputType.IsValueType && input == null)
            {
                return Activator.CreateInstance(outputType);
            }
            try
            {
                return Mapper.Map(input, input.GetType(), outputType);
            }
            catch (AutoMapperMappingException)
            {
                return Mapper.DynamicMap(input, input.GetType(), outputType);
            }
        }
    }
}
