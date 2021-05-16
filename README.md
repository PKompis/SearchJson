The Search JSON is a library for searching properties recursively inside a JSON by specifying just the property name and its type. 
Only primitive types are supported and string.

This library is just a simple experiment of mine and for production usage needs further testing and benchmarking.

## Libaries used

- [Newtonsoft.Json](https://github.com/JamesNK/Newtonsoft.Json)

## Source code layout

``` ini
SerializationBenchmark/
├── SearchJson/
|   ├── SearchJsonUtils.cs
├── SearchJsonTests/
│   ├── Types/
│   |   ├── ComplexNestedObject.cs
│   |   ├── FlatObject1.cs
│   |   ├── FlatObject2.cs
│   |   ├── SimpleNestedObject.cs
│   ├── IsValidJsonTests.cs
│   ├── TrySearchItemsTests.cs
│   ├── TrySearchItemTests.cs
```

## Authors

- Kompis Panagiotis


## License

This project is licensed under the MIT License see the [LICENSE.md](https://github.com/PKompis/SearchJson/blob/main/LICENSE) file for details.