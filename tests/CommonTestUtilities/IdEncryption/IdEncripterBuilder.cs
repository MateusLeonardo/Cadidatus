using Sqids;

namespace CommonTestUtilities.IdEncryption;

public class IdEncripterBuilder
{
    public static SqidsEncoder<int> Build()
    {
        return new SqidsEncoder<int>(new SqidsOptions
        {
            MinLength = 3,
            Alphabet = "k3G7QAe51FCsPW92uEOyq4Bg6Sp8YzVTmnU0liwDdHXLajZrfxNhobJIRcMvKt"
        });
    }
}