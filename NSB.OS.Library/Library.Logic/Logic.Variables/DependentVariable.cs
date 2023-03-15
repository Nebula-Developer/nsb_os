
namespace NSB.OS.Logic.Variables;

public class DependentVariable {
    public Func<dynamic> Get { get; set; }
    public Action<dynamic> Set { get; set; }
    public dynamic? Value { get; set; }

    // When we call var = ..., we call the set function
    public dynamic? this[dynamic? val] {
        get => GetDynamic();
        set => Set(val);
    }

    public dynamic GetDynamic() {
        return Get?.Invoke() ?? Value ?? 0;
    }

    public DependentVariable(dynamic? value = null, Func<dynamic>? get = null, Action<dynamic>? set = null) {
        Value = value;
        if (get != null) Get = get;
        else Get = () => Value ?? 0;
        if (set != null) Set = set;
        else Set = (dynamic val) => { };
    }

    public DependentVariable(Func<dynamic>? get = null, Action<dynamic>? set = null) {
        if (get != null) Get = get;
        else Get = () => Value ?? 0;
        if (set != null) Set = set;
        else Set = (dynamic val) => { };
    }

    public static implicit operator int(DependentVariable variable) => variable.GetDynamic() ?? 0;
    public static implicit operator float(DependentVariable variable) => variable.GetDynamic() ?? 0.0f;
    public static implicit operator double(DependentVariable variable) => variable.GetDynamic() ?? 0.0;
    public static implicit operator string(DependentVariable variable) => variable.GetDynamic() ?? "";
    public static implicit operator bool(DependentVariable variable) => variable.GetDynamic() ?? false;
    public static implicit operator char(DependentVariable variable) => variable.GetDynamic() ?? '\0';
    public static implicit operator byte(DependentVariable variable) => variable.GetDynamic() ?? 0;
    public static implicit operator short(DependentVariable variable) => variable.GetDynamic() ?? 0;
    public static implicit operator long(DependentVariable variable) => variable.GetDynamic() ?? 0;
}
