# DeepFinder (딥파인더)

## Introduction
딥파인더는 인공지능 딥러닝 알고리즘을 쉽게 활용할 수 있는 툴 셋입니다. 딥파인더를 이용하면 과거 데이터를 내려받고 자신만의 주식 알고리즘을 적용하여 훈련시킴으로써 주식 종목에 대한 인사이트를 얻을 수 있습니다.

## Install

DFINDER_BIN 환경변수를 설정합니다. - 실행파일이 있는 
DFINDER_DATA 환경변수를 설정합니다. - 데이터가 저장될 폴더

bin 폴더를 PATH 환경변수에 등록합니다.


## Tutorial

훈련용 데이타 DB를 받기 위해 아래 명령을 실행합니다.

dfdata

C:\temp>dfdata

전체 498M 중 498M 수신중.
D:\dev\github\primespace\data\candle_202004170000.db 훈련용 데이터 다운로드 완료.
해쉬값은 a483f4e6641247a5f5608b550ec6fa42 입니다.

데이터가 완료되면 훈련을 위한 모델 코드를 작성합니다.


메모장이나 선호하는 에디터를 이용하여 아래 코드를 작성합니다.


```csharp
using DFCore;
using ModelCore;
using ModelCore.Base;

class MyModel : ClassModel {

    public MyModel () : base (100) {
        this.Title = "MyModel";
        this.Desc = "과거 60일 시.고.저.종.거래량을 이용하여 다음날 20% 이상 상승한 종목들을 훈련시킨다.";

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
```
model_100.cs 파일이름으로 저장합니다.
이제 훈련을 시켜보겠습니다.
```
dftrainer Model_100.cs Model_100
```


```
public enum CandleDataType
{
    Open, High, Low, Close, AvgPrice, Ratio, Volume, Amount
}
```
특성들은 다음과 같습니다.

* OpenFeature(int offset) : 시가
* HighFeature(int offset) : 고가
* LowFeature(int offset) : 저가
* CloseFeature(int offset) : 종가
* VolumeFeature(int offset) : 거래량
* AccmBuyCountFeature(int offset) : 누적 매수량(일 데이타만 제공)
* AccmSelCountFeature(int offset) : 누적 매도량(일 데이타만 제공)
* StockCountFeature(int offset) : 주식수
* ForeignCountFeature(int offset) : 외국인 보유 수량
* ForeignRatioFeature(int offset) : 외국인 보유 비율
* OrganBuyCountFeature(int offset) : 기관 매수 수량
* OrganAccmBuyCountFeature(int offset) : 기관 누적 수량
* VolumeRatioFeature(int offset) : 거래량 

+ BBandFeature(CandleDataType candleDataType, int period, int dev) : 볼린저밴드
+ CciFeature(int period, int offset) : CCI
+ MacdFeature(int optInFastPeriod, int optInSlowPeriod, int optInSignalPeriod, int offset) : Macd
+ ObvFeature(int offset) : Obv
+ RsiFeature(int period, int offset) : Rsi
+ SmaFeature(int period, int offset) : Sma




