## Fake it until you make it
We have already a `tests list` provided:
```text
1 - I
5 - V
10 - X
50 - L
100 - C
500 - D
1000 - M
13 - XIII
4 - IV
2499 - MMCDXCIX
```

But this sampling can be improved regarding the problem instructions. 

What about this business rule: `The code must be able to take decimals up to 3999 and convert to their roman equivalent.`

What do we do for 0? for -1? -1000? 4000?

Let's add some tests in our list based on this rule:
```text
0 - None
-100 - None
4000 - None
1 - I
5 - V
10 - X
50 - L
100 - C
500 - D
1000 - M
13 - XIII
4 - IV
2499 - MMCDXCIX
```

:red_circle: Write a first test.
1 option is to start by a non-passing example: 
- it will force us to think in terms of public contract/api of our class
- we have directly to ask ourselves how we would like to represent potential errors (constrain inputs, extend output, continuation functions, throw an exception, ...)
 
```java
@Test
void generate_none_for_0() {
    Optional<String> result = roman.numerals.RomanNumerals.convert(0);
    assertThat(result)
            .isEmpty();
}

// Generate code from test
public class RomanNumerals {
    public static Optional<String> convert(int decimalNumber) {
        return null;
    }
}
```

:green_circle: Pass it green as fast as possible.
```java
public static Optional<String> convert(int decimalNumber) {
    return Optional.empty();
}
```

Update our tests list:
```text
✅ 0 - None
-100 - None
4000 - None
1 - I
5 - V
10 - X
50 - L
100 - C
500 - D
1000 - M
13 - XIII
4 - IV
2499 - MMCDXCIX
```

:large_blue_circle: Improve our test (inline)
```java
@Test
void generate_none_for_0() {
    assertThat(convert(0))
            .isEmpty();
}
```

:red_circle: Write another failing test, a "passing" one this time.
```java
@Test
void generate_I_for_1() {
    assertThat(convert(1))
            .isPresent()
            .contains("I");
}
```

:green_circle: Pass it green as fast as possible.
What is the fastest path to green?
```java
public static Optional<String> convert(int decimalNumber) {
    return (decimalNumber == 1)
            ? of("I")
            : empty();
}
```

```text
✅ 0 - None
-100 - None
4000 - None
✅ 1 - I
5 - V
10 - X
50 - L
100 - C
500 - D
1000 - M
13 - XIII
4 - IV
2499 - MMCDXCIX
```

:large_blue_circle: Any refactoring?

:red_circle: `5 - V`
```java
@Test
void generate_V_for_5() {
    assertThat(convert(5))
            .isPresent()
            .contains("V");
}
```

:green_circle: Add the new case
```java
public static Optional<String> convert(int decimalNumber) {
    if (decimalNumber == 1) return of("I");
    else if (decimalNumber == 5) return of("V");
    return empty();
}
```

```text
✅ 0 - None
-100 - None
4000 - None
✅ 1 - I
✅ 5 - V
10 - X
50 - L
100 - C
500 - D
1000 - M
13 - XIII
4 - IV
2499 - MMCDXCIX
```

:large_blue_circle: We have some duplication in our test code. Let's do something about it!!!
```java
class RomanNumeralsTest {
    @Test
    void generate_none_for_0() {
        assertThat(convert(0))
                .isEmpty();
    }

    // Duplication in assertions
    @Test
    void generate_I_for_1() {
        assertThat(convert(1))
                .isPresent()
                .contains("I");
    }

    @Test
    void generate_V_for_5() {
        assertThat(convert(5))
                .isPresent()
                .contains("V");
    }
}
```

Let's `extract local variables` then `extract method`:
```java
    @Test
    void generate_I_for_1() {
        final var number = 1;
        final var expectedRoman = "I";

        assertRomanConversion(number, expectedRoman);
    }

    @Test
    void generate_V_for_5() {
        assertRomanConversion(5, "V");
    }

    private void assertRomanConversion(int number, String expectedRoman) {
        assertThat(convert(number))
                .isPresent()
                .contains(expectedRoman);
    }
``` 

What can still be improved? we could use a `parameterized test` that will contain every passing examples.

> Be careful of never having conditional statements in this kind of tests. Isolate test cases inside different unit tests.

```java
class RomanNumeralsTest {
    private static Stream<Arguments> passingExamples() {
        return of(
                Arguments.of(1, "I"),
                Arguments.of(5, "V")
        );
    }

    @Test
    void generate_none_for_0() {
        assertThat(convert(0))
                .isEmpty();
    }

    @ParameterizedTest()
    @MethodSource("passingExamples")
    void generate_roman_for_decimal_numbers(int number, String expectedRoman) {
        assertThat(convert(number))
                .isPresent()
                .contains(expectedRoman);
    }
}
```

`Parameterized test` is the perfect fit for this kind of tests on [`pure functions`](https://xtrem-tdd.netlify.app/Flavours/pure-function)

After a few cycles by still applying `Fake it until you make it` approach you should have your code looking like this:

```java
public static Optional<String> convert(int decimalNumber) {
    return switch (decimalNumber) {
        case 1 -> of("I");
        case 4 -> of("IV");
        case 5 -> of("V");
        case 10 -> of("X");
        case 13 -> of("XIII");
        case 50 -> of("L");
        case 100 -> of("C");
        case 500 -> of("D");
        case 1000 -> of("M");
        default -> empty();
    };
}
```

And our tests list:
```text
✅ 0 - None
-100 - None
4000 - None
✅ 1 - I
✅ 5 - V
✅ 10 - X
✅ 50 - L
✅ 100 - C
✅ 500 - D
✅ 1000 - M
13 - XIII
4 - IV
2499 - MMCDXCIX
```

## Triangulation
:red_circle: Now that we already have implemented and hardcoded the logic we can isolate the rules.
We have 2 fascinating cases here with `4` and `13`...
Before working on `13`, let's start with a simplest one: `3`

```java
private static Stream<Arguments> passingExamples() {
    return of(
            Arguments.of(1, "I"),
            Arguments.of(3, "III"),
            Arguments.of(5, "V"),
            Arguments.of(10, "X"),
            Arguments.of(50, "L"),
            Arguments.of(100, "C"),
            Arguments.of(500, "D"),
            Arguments.of(1000, "M")
    );
}
```

:green_circle: For numbers lower than 4, we need to simply concatenate "I" together.
```java
public static Optional<String> convert(int decimalNumber) {
    if (decimalNumber <= 3) {
        var roman = "";
        for (int i = 0; i < decimalNumber; i++) {
            roman += "I";
        }
        return of(roman);
    }

    return switch (decimalNumber) {
        case 1 -> of("I");
        case 5 -> of("V");
        case 10 -> of("X");
        case 50 -> of("L");
        case 100 -> of("C");
        case 500 -> of("D");
        case 1000 -> of("M");
        default -> empty();
    };
}
```

By implementing it like this, we have an impact on the `0 case`.
Our priority is to fix it asap.

```java
public static Optional<String> convert(int decimalNumber) {
    if (decimalNumber == 0)
        return empty();

    if (decimalNumber <= 3) {
        var roman = "";
        for (int i = 0; i < decimalNumber; i++) {
            roman += "I";
        }
        return of(roman);
    }


    return switch (decimalNumber) {
        case 1 -> of("I");
        case 5 -> of("V");
        case 10 -> of("X");
        case 50 -> of("L");
        case 100 -> of("C");
        case 500 -> of("D");
        case 1000 -> of("M");
        default -> empty();
    };
}
```

:large_blue_circle: We have a lot of refactoring to make in our production code
Reduce complexity of the `convert` method by extracting some logic:
```java
public static Optional<String> convert(int decimalNumber) {
    return notInRange(decimalNumber)
            ? empty()
            : convertSafely(decimalNumber);
}

private static Optional<String> convertSafely(int decimalNumber) {
    if (decimalNumber <= 3) {
        var roman = "";
        for (int i = 0; i < decimalNumber; i++) {
            roman += "I";
        }
        return of(roman);
    }

    return switch (decimalNumber) {
        case 1 -> of("I");
        case 5 -> of("V");
        case 10 -> of("X");
        case 50 -> of("L");
        case 100 -> of("C");
        case 500 -> of("D");
        case 1000 -> of("M");
        default -> empty();
    };
}

private static boolean notInRange(int decimalNumber) {
    return decimalNumber == 0;
}
```

Remove duplication (`case 1`) and simplify loop:
```java
public static Optional<String> convert(int decimalNumber) {
    return notInRange(decimalNumber)
            ? empty()
            : convertSafely(decimalNumber);
}

private static Optional<String> convertSafely(int decimalNumber) {
    if (decimalNumber <= 3) {
        return of("I".repeat(decimalNumber));
    }

    return switch (decimalNumber) {
        case 5 -> of("V");
        case 10 -> of("X");
        case 50 -> of("L");
        case 100 -> of("C");
        case 500 -> of("D");
        case 1000 -> of("M");
        default -> empty();
    };
}

private static boolean notInRange(int decimalNumber) {
    return decimalNumber == 0;
}
```


:red_circle: Let's add another interesting case. What happens for `7`?
```java
Arguments.of(7, "VII"),
```

:green_circle: Let's focus on test cases lower than 10
```java
private static Optional<String> convertSafely(int decimalNumber) {
    if (decimalNumber < 10) {
        var roman = "";
        var remaining = decimalNumber;

        if (decimalNumber >= 5) {
            roman += "V";
            remaining -= 5;
        }
        return of(roman + "I".repeat(remaining));
    }

    return switch (decimalNumber) {
        case 5 -> of("V");
        case 10 -> of("X");
        case 50 -> of("L");
        case 100 -> of("C");
        case 500 -> of("D");
        case 1000 -> of("M");
        default -> empty();
    };
}
```

:large_blue_circle: What can be simplified?

Where are we?

```text
✅ 0 - None
-100 - None
4000 - None
✅ 1 - I
✅ 5 - V
✅ 10 - X
✅ 50 - L
✅ 100 - C
✅ 500 - D
✅ 1000 - M
✅ 3 - III
✅ 7 - VII
13 - XIII
4 - IV
2499 - MMCDXCIX
```

:red_circle: Add the `13` case
```java
Arguments.of(13, "XIII"),
```

:green_circle: Make it pass for values under 50
```java
private static Optional<String> convertSafely(int decimalNumber) {
    if (decimalNumber < 50) {
        var roman = "";
        var remaining = decimalNumber;

        if (remaining >= 10) {
            roman += "X";
            remaining -= 10;
        }
        if (remaining >= 5) {
            roman += "V";
            remaining -= 5;
        }
        return of(roman + "I".repeat(Math.max(0, remaining)));
    }

    return switch (decimalNumber) {
        case 10 -> of("X");
        case 13 -> of("XIII");
        case 50 -> of("L");
        case 100 -> of("C");
        case 500 -> of("D");
        case 1000 -> of("M");
        default -> empty();
    };
}
```

## Use a Map
:large_blue_circle: remove cases from switch and remove duplication by introducing a `Map`.
```java
public class RomanNumerals {
    private static final Map<Integer, String> decimalToNumerals = Map.of(
            10, "X",
            5, "V"
    );

    public static Optional<String> convert(int decimalNumber) {
        return notInRange(decimalNumber)
                ? empty()
                : convertSafely(decimalNumber);
    }

    private static Optional<String> convertSafely(int input) {
        if (input < 50) {
            StringBuilder roman = new StringBuilder();
            var remaining = input;

            for (var decimalToNumber : decimalToNumerals.entrySet()) {
                if (remaining >= decimalToNumber.getKey()) {
                    roman.append(decimalToNumber.getValue());
                    remaining -= decimalToNumber.getKey();
                }
            }
            return of(roman + "I".repeat(Math.max(0, remaining)));
        }

        return switch (input) {
            case 50 -> of("L");
            case 100 -> of("C");
            case 500 -> of("D");
            case 1000 -> of("M");
            default -> empty();
        };
    }

    private static boolean notInRange(int decimalNumber) {
        return decimalNumber == 0;
    }
}
```

Could we handle the case `I` with the same technique?
```java
private static final Map<Integer, String> decimalToNumerals = Map.of(
        10, "X",
        5, "V",
        // Add entry
        1, "I"
);
...

private static Optional<String> convertSafely(int input) {
    if (input < 50) {
        StringBuilder roman = new StringBuilder();
        var remaining = input;

        for (var decimalToNumber : decimalToNumerals.entrySet()) {
            // Loop on the remaining
            while (remaining >= decimalToNumber.getKey()) {
                roman.append(decimalToNumber.getValue());
                remaining -= decimalToNumber.getKey();
            }
        }
        return of(roman.toString());
    }

    return switch (input) {
        case 50 -> of("L");
        case 100 -> of("C");
        case 500 -> of("D");
        case 1000 -> of("M");
        default -> empty();
    };
}
```

What about other cases? 
```java
public final class RomanNumerals {
    private static final Map<Integer, String> decimalToNumerals = createMapForDecimalToNumerals();

    private static TreeMap<Integer, String> createMapForDecimalToNumerals() {
        var map = new TreeMap<Integer, String>(Comparator.reverseOrder());
        map.put(1000, "M");
        map.put(500, "D");
        map.put(100, "C");
        map.put(50, "L");
        map.put(10, "X");
        map.put(5, "V");
        map.put(1, "I");

        return map;
    }

    public static Optional<String> convert(int decimalNumber) {
        return notInRange(decimalNumber)
                ? empty()
                : of(convertSafely(decimalNumber));
    }

    private static String convertSafely(int input) {
        StringBuilder roman = new StringBuilder();
        var remaining = input;

        for (var decimalToNumber : decimalToNumerals.entrySet()) {
            while (remaining >= decimalToNumber.getKey()) {
                roman.append(decimalToNumber.getValue());
                remaining -= decimalToNumber.getKey();
            }
        }
        return roman.toString();
    }

    private static boolean notInRange(int decimalNumber) {
        return decimalNumber == 0;
    }
}
```

Our production code is now much more generic, it is the effect we call `Triangulation` with TDD:
- Only generalize code when we have two examples or more
- `The more specific tests you write, the more the code will become generic`

```text
✅ 0 - None
-100 - None
4000 - None
✅ 1 - I
✅ 5 - V
✅ 10 - X
✅ 50 - L
✅ 100 - C
✅ 500 - D
✅ 1000 - M
✅ 3 - III
✅ 7 - VII
✅ 13 - XIII
4 - IV
2499 - MMCDXCIX
```

:red_circle: Let's work on the `4` now. 4 is a special case...
```java
Arguments.of(4, "IV"),
```

:green_circle: Let's add it in our converter.
```java
private static TreeMap<Integer, String> createMapForDecimalToNumerals() {
    var map = new TreeMap<Integer, String>(Comparator.reverseOrder());
    map.put(1000, "M");
    map.put(500, "D");
    map.put(100, "C");
    map.put(50, "L");
    map.put(10, "X");
    map.put(5, "V");
    map.put(4, "IV");
    map.put(1, "I");

    return map;
}
```

:large_blue_circle: Anything to refactor?

Let's identify every special cases like the 4: 9, 40, 90, 400, 900.
After a few cycles, you code may look like this:
```java
public final class RomanNumerals {
    private static final Map<Integer, String> decimalToNumerals = createMapForDecimalToNumerals();

    private static TreeMap<Integer, String> createMapForDecimalToNumerals() {
        var map = new TreeMap<Integer, String>(Comparator.reverseOrder());
        map.put(1000, "M");
        map.put(900, "CM");
        map.put(500, "D");
        map.put(400, "CD");
        map.put(100, "C");
        map.put(90, "XC");
        map.put(50, "L");
        map.put(40, "XL");
        map.put(10, "X");
        map.put(9, "IX");
        map.put(5, "V");
        map.put(4, "IV");
        map.put(1, "I");

        return map;
    }

    public static Optional<String> convert(int decimalNumber) {
        return notInRange(decimalNumber)
                ? empty()
                : of(convertSafely(decimalNumber));
    }

    private static String convertSafely(int input) {
        StringBuilder roman = new StringBuilder();
        var remaining = input;

        for (var decimalToNumber : decimalToNumerals.entrySet()) {
            while (remaining >= decimalToNumber.getKey()) {
                roman.append(decimalToNumber.getValue());
                remaining -= decimalToNumber.getKey();
            }
        }
        return roman.toString();
    }

    private static boolean notInRange(int decimalNumber) {
        return decimalNumber == 0;
    }
}
```

If we did our job well, we should be able to add the latest passing example and the test should be green:
```text
✅ 0 - None
-100 - None
4000 - None
✅ 1 - I
✅ 5 - V
✅ 10 - X
✅ 50 - L
✅ 100 - C
✅ 500 - D
✅ 1000 - M
✅ 3 - III
✅ 7 - VII
✅ 13 - XIII
✅ 4 - IV
✅ 2499 - MMCDXCIX
```

## Other non-passing examples
:red_circle: Let's improve our confidence in our algorithm by covering non passing examples.
```java
private static Stream<Arguments> nonPassingExamples() {
    return of(
            Arguments.of(0),
            Arguments.of(-100),
            Arguments.of(4000)
    );
}

@ParameterizedTest()
@MethodSource("nonPassingExamples")
void generate_none_for_numbers_outside_of_range(int number) {
    assertThat(convert(number))
            .isEmpty();
}
```

:green_circle: Make it green by changing our `notInRange` method:
```java
public static Optional<String> convert(int input) {
    return notInRange(input)
            ? empty()
            : of(convertSafely(input));
}

private static String convertSafely(int input) {
    StringBuilder roman = new StringBuilder();
    var remaining = input;

    for (var decimalToNumber : decimalToNumerals.entrySet()) {
        while (remaining >= decimalToNumber.getKey()) {
            roman.append(decimalToNumber.getValue());
            remaining -= decimalToNumber.getKey();
        }
    }
    return roman.toString();
}

private static boolean notInRange(int input) {
    return input <= 0 || input > 3999;
}
```

:large_blue_circle: Anything to refactor?
```java
public static Optional<String> convert(int input) {
    // Invert the logic to make it more readable (team convention)
    return inRange(input)
            ? of(convertSafely(input))
            : empty();
}

private static String convertSafely(int input) {
    StringBuilder roman = new StringBuilder();
    var remaining = input;

    for (var decimalToNumber : decimalToNumerals.entrySet()) {
        while (remaining >= decimalToNumber.getKey()) {
            roman.append(decimalToNumber.getValue());
            remaining -= decimalToNumber.getKey();
        }
    }
    return roman.toString();
}

private static boolean inRange(int input) {
    // Extract constant
    return isPositive(input) && input <= MAX_SUPPORTED_NUMBER;
}

private static boolean isPositive(int input) {
    return input > 0;
}
```

```text
✅ 0 - None
✅ -100 - None
✅ 4000 - None
✅ 1 - I
✅ 5 - V
✅ 10 - X
✅ 50 - L
✅ 100 - C
✅ 500 - D
✅ 1000 - M
✅ 3 - III
✅ 7 - VII
✅ 13 - XIII
✅ 4 - IV
✅ 2499 - MMCDXCIX
```

## Use TreeMap keyFloor
:large_blue_circle: Anything to improve?
We can simplify our code by:
- using internal mechanism of the `TreeMap`: [floorKey](https://www.geeksforgeeks.org/treemap-floorkey-in-java-with-examples/)
  - Help us retrieve the closest lower entry for a given number
- recursively call our method

```java
public final class RomanNumerals {
    public static final int MAX_SUPPORTED_NUMBER = 3999;
    private static final TreeMap<Integer, String> decimalToNumerals = new TreeMap<>();

    static {
        decimalToNumerals.put(1000, "M");
        decimalToNumerals.put(900, "CM");
        decimalToNumerals.put(500, "D");
        decimalToNumerals.put(400, "CD");
        decimalToNumerals.put(100, "C");
        decimalToNumerals.put(90, "XC");
        decimalToNumerals.put(50, "L");
        decimalToNumerals.put(40, "XL");
        decimalToNumerals.put(10, "X");
        decimalToNumerals.put(9, "IX");
        decimalToNumerals.put(5, "V");
        decimalToNumerals.put(4, "IV");
        decimalToNumerals.put(1, "I");
    }

    public static Optional<String> convert(int input) {
        return inRange(input)
                ? of(convertSafely(input))
                : empty();
    }

    private static String convertSafely(int number) {
        var floorKey = decimalToNumerals.floorKey(number);

        return number == floorKey
                ? decimalToNumerals.get(number)
                : decimalToNumerals.get(floorKey) + convertSafely(number - floorKey);
    }

    private static boolean inRange(int input) {
        return isPositive(input) && input <= MAX_SUPPORTED_NUMBER;
    }

    private static boolean isPositive(int input) {
        return input > 0;
    }
}
```

## Use Property-Based Testing
For non-passing tests we could replace them with `property-based tests`:

```xml
<dependency>
    <groupId>com.pholser</groupId>
    <artifactId>junit-quickcheck-core</artifactId>
    <version>${junit-quickcheck.version}</version>
    <scope>test</scope>
</dependency>
<dependency>
    <groupId>com.pholser</groupId>
    <artifactId>junit-quickcheck-generators</artifactId>
    <version>${junit-quickcheck.version}</version>
    <scope>test</scope>
</dependency>
```
    
```java
@RunWith(JUnitQuickcheck.class)
public class RomanNumeralsPropertiesTest {
    private static void convertShouldBeEmpty(int number) {
        assertThat(convert(number)).isEmpty();
    }

    @Property
    public void returns_none_for_numbers_lower_or_equals_0(@InRange(max = "0") int number) {
        convertShouldBeEmpty(number);
    }

    @Property
    public void returns_none_for_numbers_greater_than_3999(@InRange(min = "3999") int number) {
        convertShouldBeEmpty(number);
    }
}
```

## Reflect
- How many times did you debug your code?
- What do you think about triangulation?
- What would you do differently?