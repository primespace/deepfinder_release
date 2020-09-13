using DFCore;
using ModelCore;
using ModelCore.Base;

class MyModel : ClassModel {

    public MyModel () : base (100) {
        this.Title = "MyModel";
        this.Desc = "60일 시.고.저.종.거래량을 이용하여 다음날 20% 이상 상승한 종목을 훈련시킨다.";

        this.PeriodType = CandlePeriodType.D;

        this.Layers.Add (32);
        this.Layers.Add (16);
        this.Layers.Add (8);

        this.Epochs = 100;
        this.BatchSize = 100;
        this.EndOffset = 10;

        for (int i = 0; i <= 60; ++i) {
            AddFeature (new OpenFeature (offset: -i));
            AddFeature (new HighFeature (offset: -i));
            AddFeature (new LowFeature (offset: -i));
            AddFeature (new CloseFeature (offset: -i));
            AddFeature (new VolumeFeature (offset: -i));
        }
    }

    protected override int GetClass (CandleDataList candleDataList, int index) {
        Candle c = candleDataList.GetCandle (index + 1);
        Candle c1 = candleDataList.GetCandle (index);

        // c : c1 = x : 100
        // 다음날 20% 이상 상승이면 1를 리턴한다.
        double x = (c.Close / c1.Close) * 100;

        if (120 < x) {
            return 1;
        } else {
            return 0;
        }
    }
}