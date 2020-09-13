class Model_100 : ClassModel {

    Model_100 () : base (100) {

        Desc = "30일동안의 종가 데이터로 다음날 20% 이상 상승하는 종목을 훈련시킨다.";
        Title = "Model 100";

        Layers.Add (30); // Dense Layer
        Layers.Add (20); // Dense Layer
        Layers.Add (10); // Dense Layer

        for (int i = 0; i < 30; ++i) {
            AddFeature (CloseFrature (offset: -i));
        }
    }

    int GetClass (CandleDataList candleDataList, int index) {
        Candle c = candleDataList.GetCandle (index + 1);
        Candle c1 = candleDataList.GetCandle (index);

        // c : c1 = x : 100
        // 다음날 20% 이상 상승이면 1를 리턴한다.
        double x = c.Close * 100 / c1.Close;

        if (20 < x) {
            return 1;
        } else {
            return 0;
        }
    }
}