/* Trigger: ALBUMS_AIU0 */
CREATE TRIGGER ALBUMS_AIU0 FOR ALBUMS
    ACTIVE AFTER INSERT OR UPDATE POSITION 0
                                                              as
declare variable id_univers type of column series_univers.id_univers;
begin
    if (old.id_serie is distinct from new.id_serie) then
begin
        if (old.id_serie is not null) then
update albums_univers set
    source_serie = 0
where
        id_album = new.id_album;

if (new.id_serie is not null) then
            for
select
    id_univers
from
    series_univers
where
        id_serie = new.id_serie
    into
                    :id_univers
            do
update or insert into albums_univers (
    id_album, id_univers, source_serie
) values (
    new.id_album, :id_univers, 1
    );
end
end;

/* Trigger: ALBUMS_UNIVERS_AU0 */
CREATE TRIGGER ALBUMS_UNIVERS_AU0 FOR ALBUMS_UNIVERS
    ACTIVE AFTER UPDATE POSITION 0
                                                           as
begin
    if (new.source_serie = 0 and new.source_album = 0) then
delete from albums_univers where id_album = new.id_album and id_univers = new.id_univers;
end;

/* Trigger: COLLECTIONS_EDITIONS_REF */
CREATE TRIGGER COLLECTIONS_EDITIONS_REF FOR COLLECTIONS
    ACTIVE AFTER UPDATE OR DELETE POSITION 0
    as
begin
    if (deleting) then update editions set id_collection = null where id_collection = old.id_collection;
if (updating) then update editions set id_collection = new.id_collection, id_editeur = new.id_editeur where id_collection = old.id_collection;
end;;

/* Trigger: COLLECTIONS_SERIE_REF */
CREATE TRIGGER COLLECTIONS_SERIE_REF FOR COLLECTIONS
    ACTIVE AFTER UPDATE OR DELETE POSITION 0
    as
begin
    if (deleting) then update series set id_collection = null where id_collection = old.id_collection;
if (updating) then update series set id_collection = new.id_collection, id_editeur = new.id_editeur where id_collection = old.id_collection;
end;;

/* Trigger: EDITEURS_SERIE_REF */
CREATE TRIGGER EDITEURS_SERIE_REF FOR EDITEURS
    ACTIVE AFTER UPDATE OR DELETE POSITION 0
    as
begin
    if (deleting) then update series set id_editeur = null where id_editeur = old.id_editeur;
if (updating) then update series set id_editeur = new.id_editeur where id_editeur = old.id_editeur;
end;;

/* Trigger: EDITIONS_AD0 */
CREATE TRIGGER EDITIONS_AD0 FOR EDITIONS
    ACTIVE AFTER DELETE POSITION 0
as
begin
delete from couvertures where id_album is null and id_edition = old.id_edition;
update couvertures set id_edition = null where id_edition = old.id_edition;

update albums set nbeditions = nbeditions - 1 where id_album = old.id_album;
end;

/* Trigger: EDITIONS_AI0 */
CREATE TRIGGER EDITIONS_AI0 FOR EDITIONS
    ACTIVE AFTER INSERT POSITION 0
as
begin
update albums set nbeditions = nbeditions + 1 where id_album = new.id_album;
end;

/* Trigger: EDITIONS_AU0 */
CREATE TRIGGER EDITIONS_AU0 FOR EDITIONS
    ACTIVE AFTER UPDATE POSITION 0
                                                     as
begin
    if (new.id_edition <> old.id_edition) then
begin
update couvertures set id_edition = new.id_edition where id_edition = old.id_edition;
end

    if (new.id_album <> old.id_album) then
begin
update albums set nbeditions = nbeditions - 1 where id_album = old.id_album;
update albums set nbeditions = nbeditions + 1 where id_album = new.id_album;
end
end;

/* Trigger: EDITIONS_BU0 */
CREATE TRIGGER EDITIONS_BU0 FOR EDITIONS
    ACTIVE BEFORE UPDATE POSITION 0
                                                      as
begin
    if (updating and new.prete <> old.prete) then new.stock = 1 - new.prete;
end;

/* Trigger: EDITIONS_COTE_BIU1 */
CREATE TRIGGER EDITIONS_COTE_BIU1 FOR EDITIONS
    ACTIVE AFTER INSERT OR UPDATE POSITION 1
                                                                     as
declare variable existprix integer;
begin
    if (new.anneecote is not null and new.prixcote is not null) then
begin
select count(prixcote) from cotes where id_edition = new.id_edition and anneecote = new.anneecote into :existprix;
if (existprix = 0) then
            insert into cotes (id_edition, anneecote, prixcote) values (new.id_edition, new.anneecote, new.prixcote);
else
update cotes set prixcote = new.prixcote where id_edition = new.id_edition and anneecote = new.anneecote;
end
end;

/* Trigger: GENRES_AD0 */
CREATE TRIGGER GENRES_AD0 FOR GENRES
    ACTIVE AFTER DELETE POSITION 0
as
begin
delete from import_associations where id = old.id_genre and typedata = 5;
end;

/* Trigger: LISTES_AUD0 */
CREATE TRIGGER LISTES_AUD0 FOR LISTES
    ACTIVE AFTER UPDATE OR DELETE POSITION 0
    as
declare variable newvalue type of column listes.ref;
begin
    if (deleting) then
        newvalue = null;
else
        newvalue = new.ref;

    if (newvalue is distinct from old.ref) then
begin
        if (old.categorie = 1) then
begin
update editions set etat = :newvalue where etat = old.ref;
update series set etat = :newvalue where etat = old.ref;
end
        if (old.categorie = 2) then
begin
update editions set reliure = :newvalue where reliure = old.ref;
update series set reliure = :newvalue where reliure = old.ref;
end
        if (old.categorie = 3) then
begin
update editions set typeedition = :newvalue where typeedition = old.ref;
update series set typeedition = :newvalue where typeedition = old.ref;
end
        if (old.categorie = 4) then
begin
update editions set orientation = :newvalue where orientation = old.ref;
update series set orientation = :newvalue where orientation = old.ref;
end
        if (old.categorie = 5) then
begin
update editions set formatedition = :newvalue where formatedition = old.ref;
update series set formatedition = :newvalue where formatedition = old.ref;
end
        if (old.categorie = 6) then
update couvertures set categorieimage = :newvalue where categorieimage = old.ref;
if (old.categorie = 8) then
begin
update editions set senslecture = :newvalue where senslecture = old.ref;
update series set senslecture = :newvalue where senslecture = old.ref;
end
        if (old.categorie = 9) then
begin
update albums set notation = :newvalue where notation = old.ref;
update series set notation = :newvalue where notation = old.ref;
end
end
end;

/* Trigger: SERIES_AD0 */
CREATE TRIGGER SERIES_AD0 FOR SERIES
    ACTIVE AFTER DELETE POSITION 0
as
begin
delete from albums where id_serie = old.id_serie;
end;

/* Trigger: SERIES_UNIVERS_AD0 */
CREATE TRIGGER SERIES_UNIVERS_AD0 FOR SERIES_UNIVERS
    ACTIVE AFTER DELETE POSITION 0
as
declare variable id_album type of column albums.id_album;
begin
for
select
    id_album
from
    albums
where
        id_serie = old.id_serie
    into
            :id_album
    do
update albums_univers set
    source_serie = 0
where
        id_album = :id_album
  and id_univers = old.id_univers;
end;

/* Trigger: SERIES_UNIVERS_AI0 */
CREATE TRIGGER SERIES_UNIVERS_AI0 FOR SERIES_UNIVERS
    ACTIVE AFTER INSERT POSITION 0
as
declare variable id_album type of column albums.id_album;
begin
for
select
    id_album
from
    albums
where
        id_serie = new.id_serie
    into
            :id_album
    do
update or insert into albums_univers (
    id_album, id_univers, source_serie
) values (
    :id_album, new.id_univers, 1
    );
end;

/* Trigger: UNIVERS_AD0 */
CREATE TRIGGER UNIVERS_AD0 FOR UNIVERS
    ACTIVE AFTER DELETE POSITION 0
as
begin
update univers set
    id_univers_parent = null
where
        id_univers_parent = old.id_univers;
end;

/* Trigger: UNIVERS_AU0 */
CREATE TRIGGER UNIVERS_AU0 FOR UNIVERS
    ACTIVE AFTER UPDATE POSITION 0
                                                    as
begin
    if (new.branche_univers is distinct from old.branche_univers) then
update univers set
    branche_univers = new.branche_univers || '|' || id_univers
where
        id_univers_parent = new.id_univers;
end;

/* Trigger: UNIVERS_DV */
CREATE TRIGGER UNIVERS_DV FOR UNIVERS
    ACTIVE BEFORE INSERT OR UPDATE POSITION 1
                                                              as
begin
    if (new.id_univers_parent is null) then
begin
        new.id_univers_racine = new.id_univers;
        new.branche_univers = '|' || new.id_univers || '|';
end
else if (new.id_univers_parent is distinct from old.id_univers_parent) then
select
    id_univers_racine, '|' || new.id_univers || branche_univers
from
    univers
where
        id_univers = new.id_univers_parent
    into
            new.id_univers_racine, new.branche_univers;
end;
