# DeepFinder (�����δ�)

## Introduction
�����δ��� �ΰ����� ������ �˰����� ���� Ȱ���� �� �ִ� �� ���Դϴ�. �����δ��� �̿��ϸ� ���� �����͸� �����ް� �ڽŸ��� �ֽ� �˰����� �����Ͽ� �Ʒý�Ŵ���ν� �ֽ� ���� ���� �λ���Ʈ�� ���� �� �ֽ��ϴ�.

## Install

## Tutorial

�޸����̳� ��ȣ�ϴ� �����͸� �̿��Ͽ� �Ʒ� �ڵ带 �ۼ��մϴ�.


```csharp

class Model_100 : ClassModel {

    Model_100() : base(100) {

        Desc = "30�ϵ����� ���� �����ͷ� ������ 20% �̻� ����ϴ� ������ �Ʒý�Ų��.";
        Title = "Model 100";

        Layers.Add(30); // Dense Layer
        Layers.Add(20); // Dense Layer
        Layers.Add(10); // Dense Layer

        for(int i = 0; i < 30; ++i) {
            AddFeature(CloseFrature(offset: -i));
        }
    }

    int GetClass(CandleDataList candleDataList, int index) {
        Candle c = candleDataList.GetCandle(index + 1);
        Candle c1 = candleDataList.GetCandle(index);

        // c : c1 = x : 100
        // ������ 20% �̻� ����̸� 1�� �����Ѵ�.
        double x = c.Close * 100 / c1.Close;

        if(20 < x) {
            return 1;
        } else {
            return 0;
        }
    }
}
```
model_100.cs �����̸����� �����մϴ�.
���� �Ʒ��� ���Ѻ��ڽ��ϴ�.
```
dftrainer --source ./Model_100.cs
```


```
public enum CandleDataType
{
    Open, High, Low, Close, AvgPrice, Ratio, Volume, Amount
}
```
Ư������ ������ �����ϴ�.

* OpenFeature(int offset) : �ð�
* HighFeature(int offset) : ��
* LowFeature(int offset) : ����
* CloseFeature(int offset) : ����
* VolumeFeature(int offset) : �ŷ���
* AccmBuyCountFeature(int offset) : ���� �ż���(�� ����Ÿ�� ����)
* AccmSelCountFeature(int offset) : ���� �ŵ���(�� ����Ÿ�� ����)
* StockCountFeature(int offset) : �ֽļ�
* ForeignCountFeature(int offset) : �ܱ��� ���� ����
* ForeignRatioFeature(int offset) : �ܱ��� ���� ����
* OrganBuyCountFeature(int offset) : ��� �ż� ����
* OrganAccmBuyCountFeature(int offset) : ��� ���� ����
* VolumeRatioFeature(int offset) : �ŷ��� 

+ BBandFeature(CandleDataType candleDataType, int period, int dev) : ���������
+ CciFeature(int period, int offset) : CCI
+ MacdFeature(int optInFastPeriod, int optInSlowPeriod, int optInSignalPeriod, int offset) : Macd
+ ObvFeature(int offset) : Obv
+ RsiFeature(int period, int offset) : Rsi
+ SmaFeature(int period, int offset) : Sma




