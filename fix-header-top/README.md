# Fixed Table Header Plugin for jQuery

テーブルのヘッダーをスクロール時に固定する jQueryプラグイン。

## プラグインの読み込み

```html
<script src="https://code.jquery.com/jquery-3.6.0.js"></script>
<script src="jquery.fixedTableHeader.js"></script>
```

## 使い方

```html
<table class="tableClassName">
    <tr>
        <th>th 1</th>
        <th>th 2</th>
    </tr>
    <tr>
        <td>1</td>
    </tr>
    <tr>
        <td>2</td>
    </tr>
    <tr>
        <td>3</td>
    </tr>
</table>
<script>
    jQuery(($) => {
        $(".tableClassName").fixedTableHeader();
    })
</script>
```

## オプション

`fixedTableHeader` 関数の引数に渡す。

- 第1引数にターゲットの要素 

```javascript
// デフォルト値
"th"
```

`null`, `undefined` を指定した場合、デフォルト値となる

- 第2引数にCSSのキーと値を含んだオブジェクト

```javascript
// デフォルト値
{
    position: "sticky",
    top: 0
}
```

`null`, `undefined` を指定した場合、デフォルト値となる

### Examples

```javascript
$(".tableClassName").fixedTableHeader("th", {
    top: "200px"
})
```

```javascript
$(".tableClassName").fixedTableHeader(null, {
    top: "200px"
})
```

```javascript
$(".tableClassName").fixedTableHeader(".table_header")
```

```javascript
$(".tableClassName").fixedTableHeader("th", null)
```

## Licence

The MIT License

## Author

**kiyotd**  
web designer, front-end engineer

- [kiyotd.com](https://kiyotd.com/)
- [twitter](https://twitter.com/_kiyotd)
- [github](https://github.com/kiyotd)
