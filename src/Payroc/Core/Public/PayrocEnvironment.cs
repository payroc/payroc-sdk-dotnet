namespace Payroc;

public class PayrocEnvironment
{
    public static readonly PayrocEnvironment Production = new PayrocEnvironment
    {
        Api = "https://api.payroc.com",
        Identity = "https://identity.payroc.com",
    };

    /// <summary>
    /// URL for the api service
    /// </summary>
    public string Api { get;
#if NET5_0_OR_GREATER
        init;
#else
        set;
#endif
    }

    /// <summary>
    /// URL for the identity service
    /// </summary>
    public string Identity { get;
#if NET5_0_OR_GREATER
        init;
#else
        set;
#endif
    }
}
