﻿namespace rt.atl.business.exchange.impl
{
  using System;
  using System.Collections.Generic;
  using System.Linq;

  /// <summary>
  ///  Перекодирует ошибки СРЗ в более вменяемые
  /// </summary>
  public class RecodingErros
  {
    /// <summary>
    /// The new errors.
    /// </summary>
    private static readonly List<Tuple<int, string>> NewErrors = new List<Tuple<int, string>>()
                                                          {
                                                            new Tuple<int, string>(4, "Дата рождения застрахованного лица меньше 1900 года. Обратитесь к администратору"),
                                                            new Tuple<int, string>(6, "Ошибка в контрольном числе СНИЛС застрахованного лица"),
                                                            new Tuple<int, string>(10, "Район регистрации застрахованного лица отсутствует в адресном справочнике СРЗ. Обратитесь к администратору"),
                                                            new Tuple<int, string>(11, "Район пребывания застрахованного лица отсутствует в адресном справочнике СРЗ. Обратитесь к администратору"),
                                                            new Tuple<int, string>(16, "Регион регистрации застрахованного лица отсутствует в адресном справочнике СРЗ. Обратитесь к администратору"),
                                                            new Tuple<int, string>(17, "Регион пребывания застрахованного лица отсутствует в адресном справочнике СРЗ. Обратитесь к администратору"),
                                                            new Tuple<int, string>(18, "ЕНП не соответствует полу и возрасту застрахованного"),
                                                            new Tuple<int, string>(19, "Ошибка в контрольном числе ЕНП застрахованного лица"),
                                                            new Tuple<int, string>(21, "Имя застрахованного лица не соответствует полу"),
                                                            new Tuple<int, string>(22, "Отчество застрахованного лица не соответствует полу"),
                                                            new Tuple<int, string>(33, "Район регистрации застрахованного лица не соответствует региону по данным адресного справочника СРЗ. Обратитесь к администратору"),
                                                            new Tuple<int, string>(34, "Срок действия временного свидетельства не равен 30 рабочим дням. Обратитесь к администратору для инициализации производственного календаря"),
                                                            new Tuple<int, string>(41, "В сводном регистре застрахованных данное лицо не найдено. Обратитесь к администратору"),
                                                            new Tuple<int, string>(43, "ЕНП принадлежит другому застрахованному лицу "),
                                                            new Tuple<int, string>(48, "Застрахованное лицо ранее осуществило первичный выбор СМО. Для него возможна замена СМО и/или замена полиса. Измените причину подачи заявления"),
                                                            new Tuple<int, string>(50, "Серия и номер документа, подтверждающего факт страхования, принадлежат другому застрахованному лицу"),
                                                            new Tuple<int, string>(53, "Заявление сформировано с нарушением правил страхования"),
                                                            new Tuple<int, string>(61, "У застрахованного лица есть закрытая страховка в данной СМО. Переоформить по ней полис невозможно"),
                                                            new Tuple<int, string>(65, "Полис застрахованного лица на изготовлении. До его выдачи прием заявлений не допускается"),
                                                            new Tuple<int, string>(67, "Документ, удостоверяющий личность, найден у другого застрахованного лица"),
                                                            new Tuple<int, string>(68, "СНИЛС найден у другого застрахованного лица"),
                                                            new Tuple<int, string>(69, "В сводном регистре застрахованных найден дубликат данного лица. Обратитесь в ТФОМС"),
                                                            new Tuple<int, string>(72, "В заявлении указаны даты действия документа, подтверждающего факт страхования, отличные от зарегистрированных в СРЗ. Проверьте корректность даты выдачи полиса и срока действия документов УДЛ "),
                                                            new Tuple<int, string>(81, "Застрахованное лицо зарегистрировано в СРЗ с другим СНИЛС. Изменение СНИЛС не допускается"),
                                                            new Tuple<int, string>(82, "Документ, удостоверяющий личность застрахованного, не совпадает с зарегистрированным в СРЗ. Изменение документа запрещено в данном типе заявления"),
                                                            new Tuple<int, string>(83, "Документ, подтверждающий факт страхования, отличается от зарегистрированного в СРЗ. Изменение ДПФС запрещено в данном типе заявления"),
                                                            new Tuple<int, string>(84, "Дата начала действия ПНО должна быть больше даты ВС"),
                                                            new Tuple<int, string>(85, "Заявление не принято по техническим причинам. Обратитесь к администратору"),
                                                            new Tuple<int, string>(86, "Недопустимая давность изменений. Запрещен прием заявлений о выборе и замене СМО ранее 01.10.2013"),
                                                          };

    /// <summary>
    /// The recoding.
    /// </summary>
    /// <param name="code">
    /// The code.
    /// </param>
    /// <param name="nameDefault">
    /// The name default.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string Recoding(int code, string nameDefault)
    {
      var t = NewErrors.FirstOrDefault(x => x.Item1 == code);
      return t != null ? t.Item2 : nameDefault;
    }
  }
}