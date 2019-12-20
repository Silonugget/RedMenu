﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedMenuClient.data
{
    public static class PedModels
    {
        // Source: https://github.com/summeryukata/redm-simple-trainer/blob/ad03efd128e1a4f2a2cf3a3b31b095432ce24122/NameArrays.cs#L15
        public static List<string> MalePedHashes { get; }
            = new List<string>()
        {
            "A_M_M_ARMCHOLERACORPSE_01",
            "A_M_M_ARMTOWNFOLK_01",
            "A_M_M_ARMTOWNFOLK_02",
            "A_M_M_ASBMINER_01",
            "A_M_M_ASBMINER_02",
            "A_M_M_ASBMINER_03",
            "A_M_M_ASBMINER_04",
            "A_M_M_ASBTOWNFOLK_01",
            "A_M_M_BIVWORKER_01",
            "A_M_M_BLWFOREMAN_01",
            "A_M_M_BLWLABORER_01",
            "A_M_M_BLWLABORER_02",
            "A_M_M_BLWOBESEMEN_01",
            "A_M_M_BLWTOWNFOLK_01",
            "A_M_M_BLWUPPERCLASS_01",
            "A_M_M_BTCHILLBILLY_01",
            "A_M_M_BTCOBESEMEN_01",
            "A_M_M_BYNROUGHTRAVELLERS_01",
            "A_M_M_EMRFARMHAND_01",
            "A_M_M_GRIROUGHTRAVELLERS_01",
            "A_M_M_GUATOWNFOLK_01",
            "A_M_M_HTLFANCYDRIVERS_01",
            "A_M_M_HTLFANCYTRAVELLERS_01",
            "A_M_M_HTLROUGHTRAVELLERS_01",
            "A_M_M_JAMESONGUARD_01",
            "A_M_M_LAGTOWNFOLK_01",
            "A_M_M_LOWERSDTOWNFOLK_01",
            "A_M_M_LOWERSDTOWNFOLK_02",
            "A_M_M_LOWERTRAINPASSENGERS_01",
            "A_M_M_MIDDLESDTOWNFOLK_01",
            "A_M_M_MIDDLESDTOWNFOLK_02",
            "A_M_M_MIDDLESDTOWNFOLK_03",
            "A_M_M_MIDDLETRAINPASSENGERS_01",
            "A_M_M_NBXDOCKWORKERS_01",
            "A_M_M_NBXLABORERS_01",
            "A_M_M_NBXSLUMS_01",
            "A_M_M_NBXUPPERCLASS_01",
            "A_M_M_NEAROUGHTRAVELLERS_01",
            "A_M_M_RANCHER_01",
            "A_M_M_RANCHERTRAVELERS_WARM_01",
            "A_M_M_RHDFOREMAN_01",
            "A_M_M_RHDOBESEMEN_01",
            "A_M_M_RHDTOWNFOLK_01_LABORER",
            "A_M_M_RHDTOWNFOLK_01",
            "A_M_M_RHDTOWNFOLK_02",
            "A_M_M_RHDUPPERCLASS_01",
            "A_M_M_RKRFANCYDRIVERS_01",
            "A_M_M_RKRFANCYTRAVELLERS_01",
            "A_M_M_RKRROUGHTRAVELLERS_01",
            "A_M_M_SCLROUGHTRAVELLERS_01",
            "A_M_M_SDCHINATOWN_01",
            "A_M_M_SDDOCKFOREMAN_01",
            "A_M_M_SDDOCKWORKERS_02",
            "A_M_M_SDLABORERS_02",
            "A_M_M_SDOBESEMEN_01",
            "A_M_M_SDSLUMS_02",
            "A_M_M_SKPPRISONER_01",
            "A_M_M_STRFANCYTOURIST_01",
            "A_M_M_STRLABORER_01",
            "A_M_M_STRTOWNFOLK_01",
            "A_M_M_TUMTOWNFOLK_01",
            "A_M_M_TUMTOWNFOLK_02",
            "A_M_M_UNIGUNSLINGER_01",
            "A_M_M_UPPERTRAINPASSENGERS_01",
            "A_M_M_VALCRIMINALS_01",
            "A_M_M_VALFARMER_01",
            "A_M_M_VALLABORER_01",
            "A_M_M_VALTOWNFOLK_01",
            "A_M_M_VALTOWNFOLK_02",
            "A_M_M_VHTTHUG_01",
            "A_M_M_VHTTOWNFOLK_01",
            "A_M_M_WAPWARRIORS_01",
            "A_M_O_BLWUPPERCLASS_01",
            "A_M_O_BTCHILLBILLY_01",
            "A_M_O_GUATOWNFOLK_01",
            "A_M_O_LAGTOWNFOLK_01",
            "A_M_O_SDCHINATOWN_01",
            "A_M_O_SDUPPERCLASS_01",
            "A_M_O_WAPTOWNFOLK_01",
            "A_M_Y_ASBMINER_01",
            "A_M_Y_ASBMINER_02",
            "A_M_Y_ASBMINER_03",
            "A_M_Y_ASBMINER_04",
            "A_M_Y_NBXSTREETKIDS_01",
            "A_M_Y_NBXSTREETKIDS_SLUMS_01",
            "A_M_Y_SDSTREETKIDS_SLUMS_02",
            "G_M_M_UNIBANDITOS_01",
            "G_M_M_UNIBRAITHWAITES_01",
            "G_M_M_UNIBRONTEGOONS_01",
            "G_M_M_UNICORNWALLGOONS_01",
            "G_M_M_UNICRIMINALS_01",
            "G_M_M_UNICRIMINALS_02",
            "G_M_M_UNIDUSTER_01",
            "G_M_M_UNIDUSTER_02",
            "G_M_M_UNIDUSTER_03",
            "G_M_M_UNIGRAYS_01",
            "G_M_M_UNIINBRED_01",
            "G_M_M_UNIMOUNTAINMEN_01",
            "G_M_O_UNIEXCONFEDS_01",
            "G_M_Y_UNIEXCONFEDS_01",
            "S_M_M_AMBIENTBLWPOLICE_01",
            "S_M_M_AMBIENTLAWRURAL_01",
            "S_M_M_AMBIENTSDPOLICE_01",
            "S_M_M_ARMY_01",
            "S_M_M_BANKCLERK_01",
            "S_M_M_BARBER_01",
            "S_M_M_BLWCOWPOKE_01",
            "S_M_M_CGHWORKER_01",
            "S_M_M_CKTWORKER_01",
            "S_M_M_CORNWALLGUARD_01",
            "S_M_M_FUSSARHENCHMAN_01",
            "S_M_M_LIVERYWORKER_01",
            "S_M_M_MAGICLANTERN_01",
            "S_M_M_MAPWORKER_01",
            "S_M_M_MARKETVENDOR_01",
            "S_M_M_RACRAILGUARDS_01",
            "S_M_M_RACRAILWORKER_01",
            "S_M_M_RHDCOWPOKE_01",
            "S_M_M_SDTICKETSELLER_01",
            "S_M_M_SKPGUARD_01",
            "S_M_M_STRLUMBERJACK_01",
            "S_M_M_TAILOR_01",
            "S_M_M_TRAINSTATIONWORKER_01",
            "S_M_M_TUMDEPUTIES_01",
            "S_M_M_UNIBUTCHERS_01",
            "S_M_M_UNITRAINENGINEER_01",
            "S_M_M_UNITRAINGUARDS_01",
            "S_M_M_VALCOWPOKE_01",
            "S_M_Y_ARMY_01",
            "S_M_Y_NEWSPAPERBOY_01",
            "S_M_Y_RACRAILWORKER_01",
            "U_M_M_ARMGENERALSTOREOWNER_01",
            "U_M_M_ARMTRAINSTATIONWORKER_01",
            "U_M_M_ARMUNDERTAKER_01",
            "U_M_M_ASBGUNSMITH_01",
            "U_M_M_BIVFOREMAN_01",
            "U_M_M_BLWTRAINSTATIONWORKER_01",
            "U_M_M_BWMSTABLEHAND_01",
            "U_M_M_CAJHOMESTEAD_01",
            "U_M_M_CRDHOMESTEADTENANT_01",
            "U_M_M_CRDHOMESTEADTENANT_02",
            "U_M_M_CZPHOMESTEADFATHER_01",
            "U_M_M_DORHOMESTEADHUSBAND_01",
            "U_M_M_EMRFATHER_01",
            "U_M_M_HTLFOREMAN_01",
            "U_M_M_HTLHUSBAND_01",
            "U_M_M_LNSWORKER_01",
            "U_M_M_LNSWORKER_02",
            "U_M_M_LNSWORKER_03",
            "U_M_M_LNSWORKER_04",
            "U_M_M_LRSHOMESTEADTENANT_01",
            "U_M_M_MFRRANCHER_01",
            "U_M_M_NBXBARTENDER_01",
            "U_M_M_NBXBARTENDER_02",
            "U_M_M_NBXGENERALSTOREOWNER_01",
            "U_M_M_NBXGUNSMITH_01",
            "U_M_M_NBXSHADYDEALER_01",
            "U_M_M_ORPGUARD_01",
            "U_M_M_RACFOREMAN_01",
            "U_M_M_RHDBARTENDER_01",
            "U_M_M_RHDGENSTOREOWNER_01",
            "U_M_M_RHDGENSTOREOWNER_02",
            "U_M_M_RHDGUNSMITH_01",
            "U_M_M_RHDSHERIFF_01",
            "U_M_M_RHDTRAINSTATIONWORKER_01",
            "U_M_M_RKFRANCHER_01",
            "U_M_M_SDPHOTOGRAPHER_01",
            "U_M_M_SDTRAPPER_01",
            "U_M_M_STRFREIGHTSTATIONOWNER_01",
            "U_M_M_STRGENSTOREOWNER_01",
            "U_M_M_STRSHERRIFF_01",
            "U_M_M_STRWELCOMECENTER_01",
            "U_M_M_TUMBARTENDER_01",
            "U_M_M_TUMBUTCHER_01",
            "U_M_M_TUMGUNSMITH_01",
            "U_M_M_UNIEXCONFEDSBOUNTY_01",
            "U_M_M_VALBARBER_01",
            "U_M_M_VALBARTENDER_01",
            "U_M_M_VALBUTCHER_01",
            "U_M_M_VALDOCTOR_01",
            "U_M_M_VALGENSTOREOWNER_01",
            "U_M_M_VALGUNSMITH_01",
            "U_M_M_VALHOTELOWNER_01",
            "U_M_M_VHTSTATIONCLERK_01",
            "U_M_M_WALGENERALSTOREOWNER_01",
            "U_M_M_WAPOFFICIAL_01",
            "U_M_O_ARMBARTENDER_01",
            "U_M_O_ASBSHERIFF_01",
            "U_M_O_BLWBARTENDER_01",
            "U_M_O_BLWGENERALSTOREOWNER_01",
            "U_M_O_BLWPOLICECHIEF_01",
            "U_M_O_CAJHOMESTEAD_01",
            "U_M_O_CMRCIVILWARCOMMANDO_01",
            "U_M_O_PSHRANCHER_01",
            "U_M_O_VALBARTENDER_01",
            "U_M_O_VHTEXOTICSHOPKEEPER_01",
            "U_M_Y_CAJHOMESTEAD_01",
            "U_M_Y_CZPHOMESTEADSON_01",
            "U_M_Y_CZPHOMESTEADSON_02",
            "U_M_Y_CZPHOMESTEADSON_03",
            "U_M_Y_EMRSON_01",
            "U_M_Y_HTLWORKER_01",
            "U_M_Y_HTLWORKER_02",
        };
        public static List<string> FemalePedHashes { get; }
            = new List<string>()
        {
            "A_F_M_ARMCHOLERACORPSE_01",
            "A_F_M_ARMTOWNFOLK_01",
            "A_F_M_ARMTOWNFOLK_02",
            "A_F_M_ASBTOWNFOLK_01",
            "A_F_M_BIVFANCYTRAVELLERS_01",
            "A_F_M_BLWTOWNFOLK_01",
            "A_F_M_BLWTOWNFOLK_02",
            "A_F_M_BLWUPPERCLASS_01",
            "A_F_M_BTCHILLBILLY_01",
            "A_F_M_BTCOBESEWOMEN_01",
            "A_F_M_FAMILYTRAVELERS_WARM_01",
            "A_F_M_GUATOWNFOLK_01",
            "A_F_M_HTLFANCYTRAVELLERS_01",
            "A_F_M_LAGTOWNFOLK_01",
            "A_F_M_LOWERSDTOWNFOLK_01",
            "A_F_M_LOWERSDTOWNFOLK_02",
            "A_F_M_LOWERSDTOWNFOLK_03",
            "A_F_M_LOWERTRAINPASSENGERS_01",
            "A_F_M_MIDDLESDTOWNFOLK_01",
            "A_F_M_MIDDLESDTOWNFOLK_02",
            "A_F_M_MIDDLETRAINPASSENGERS_01",
            "A_F_M_NBXSLUMS_01",
            "A_F_M_NBXUPPERCLASS_01",
            "A_F_M_NBXWHORE_01",
            "A_F_M_RHDPROSTITUTE_01",
            "A_F_M_RHDTOWNFOLK_01",
            "A_F_M_RHDTOWNFOLK_02",
            "A_F_M_RHDUPPERCLASS_01",
            "A_F_M_RKRFANCYTRAVELLERS_01",
            "A_F_M_SDCHINATOWN_01",
            "A_F_M_SDFANCYWHORE_01",
            "A_F_M_SDOBESEWOMEN_01",
            "A_F_M_SDSLUMS_02",
            "A_F_M_STRTOWNFOLK_01",
            "A_F_M_TUMTOWNFOLK_01",
            "A_F_M_TUMTOWNFOLK_02",
            "A_F_M_UPPERTRAINPASSENGERS_01",
            "A_F_M_VALPROSTITUTE_01",
            "A_F_M_VALTOWNFOLK_01",
            "A_F_M_VHTPROSTITUTE_01",
            "A_F_M_VHTTOWNFOLK_01",
            "A_F_M_WAPTOWNFOLK_01",
            "A_F_O_BLWUPPERCLASS_01",
            "A_F_O_BTCHILLBILLY_01",
            "A_F_O_GUATOWNFOLK_01",
            "A_F_O_LAGTOWNFOLK_01",
            "A_F_O_SDCHINATOWN_01",
            "A_F_O_SDUPPERCLASS_01",
            "A_F_O_WAPTOWNFOLK_01",
            "S_F_M_MAPWORKER_01",
            "U_F_M_HTLWIFE_01",
            "U_F_M_LAGMOTHER_01",
            "U_F_M_RKSHOMESTEADTENANT_01",
            "U_F_M_TUMGENERALSTOREOWNER_01",
            "U_F_M_VHTBARTENDER_01",
            "U_F_O_WTCTOWNFOLK_01",
            "U_F_Y_CZPHOMESTEADDAUGHTER_01",
        };

        public static List<string> CutscenePedHashes { get; }
            = new List<string>()
        {
            "CS_ABE",
            "CS_ABERDEENPIGFARMER",
            "CS_ABERDEENSISTER",
            "CS_ABIGAILROBERTS",
            "CS_ANGUSGEDDES",
            "CS_ARCHIEDOWN",
            "CS_BILLWILLIAMSON",
            "CS_BLWPHOTOGRAPHER",
            "CS_CHARLESSMITH",
            "CS_CLEET",
            "CS_CREOLECAPTAIN",
            "CS_DAVIDGEDDES",
            "CS_DOROETHEAWICKLOW",
            "CS_DUNCANGEDDES",
            "CS_DUTCH",
            "CS_EAGLEFLIES",
            "CS_EDITHDOWN",
            "CS_EDMUNDLOWRY",
            "CS_GRIZZLEDJON",
            "CS_HOSEAMATTHEWS",
            "CS_JACKMARSTON_TEEN",
            "CS_JACKMARSTON",
            "CS_JAVIERESCUELLA",
            "CS_JOE",
            "CS_JOHNMARSTON",
            "CS_JOSIAHTRELAWNY",
            "CS_KAREN",
            "CS_KIERAN",
            "CS_LENNY",
            "CS_LEOSTRAUSS",
            "CS_MARYBETH",
            "CS_MICAHBELL",
            "CS_MOLLYOSHEA",
            "CS_MRPEARSON",
            "CS_MRSADLER",
            "CS_MRSGEDDES",
            "CS_NICHOLASTIMMINS",
            "CS_POISONWELLSHAMAN",
            "CS_REVSWANSON",
            "CS_SDDOCTOR_01",
            "CS_SEAN",
            "CS_SHERIFFFREEMAN",
            "CS_SUSANGRIMSHAW",
            "CS_THOMASDOWN",
            "CS_TILLY",
            "CS_TOMDICKENS",
            "CS_UNCLE",
            "CS_VALSHERIFF",
            "CS_WROBEL",
        };

        public static List<string> OtherPedHashes { get; }
            = new List<string>()
        {
            "PLAYER_THREE",
            "PLAYER_ZERO",
            "RE_DEADBODIES_MALES_01",
        };

        public static List<string> AnimalHashes { get; }
            = new List<string>()
        {
            "A_C_ALLIGATOR_01",
            "A_C_ALLIGATOR_02",
            "A_C_ALLIGATOR_03",
            "A_C_ARMADILLO_01",
            "A_C_BADGER_01",
            "A_C_BEAR_01",
            "A_C_BEARBLACK_01",
            "A_C_BEAVER_01",
            "A_C_BIGHORNRAM_01",
            "A_C_BOAR_01",
            "A_C_BUCK_01",
            "A_C_BUFFALO_01",
            "A_C_BULL_01",
            "A_C_CALIFORNIACONDOR_01",
            "A_C_CAROLINAPARAKEET_01",
            "A_C_CAT_01",
            "A_C_CEDARWAXWING_01",
            "A_C_CHICKEN_01",
            "A_C_CHIPMUNK_01",
            "A_C_CORMORANT_01",
            "A_C_COUGAR_01",
            "A_C_COW",
            "A_C_COYOTE_01",
            "A_C_CRAB_01",
            "A_C_CRANEWHOOPING_01",
            "A_C_CROW_01",
            "A_C_DEER_01",
            "A_C_DOGAMERICANFOXHOUND_01",
            "A_C_DOGAUSTRALIANSHEPERD_01",
            "A_C_DOGBLUETICKCOONHOUND_01",
            "A_C_DOGCATAHOULACUR_01",
            "A_C_DOGCHESBAYRETRIEVER_01",
            "A_C_DOGCOLLIE_01",
            "A_C_DOGHOBO_01",
            "A_C_DOGHOUND_01",
            "A_C_DOGHUSKY_01",
            "A_C_DOGLAB_01",
            "A_C_DOGPOODLE_01",
            "A_C_DOGRUFUS_01",
            "A_C_DOGSTREET_01",
            "A_C_DONKEY_01",
            "A_C_DUCK_01",
            "A_C_EAGLE_01",
            "A_C_EGRET_01",
            "A_C_ELK_01",
            "A_C_FISHBULLHEADCAT_01_SM",
            "A_C_FISHNORTHERNPIKE_01_LG",
            "A_C_FISHRAINBOWTROUT_01_MS",
            "A_C_FISHSALMONSOCKEYE_01_MS",
            "A_C_FOX_01",
            "A_C_FROGBULL_01",
            "A_C_GILAMONSTER_01",
            "A_C_GOAT_01",
            "A_C_GOOSECANADA_01",
            "A_C_HAWK_01",
            "A_C_HERON_01",
            "A_C_IGUANA_01",
            "A_C_IGUANADESERT_01",
            "A_C_JAVELINA_01",
            "A_C_LOON_01",
            "A_C_MOOSE_01",
            "A_C_MUSKRAT_01",
            "A_C_OWL_01",
            "A_C_OX_01",
            "A_C_PANTHER_01",
            "A_C_PELICAN_01",
            "A_C_PHEASANT_01",
            "A_C_PIG_01",
            "A_C_POSSUM_01",
            "A_C_PRONGHORN_01",
            "A_C_QUAIL_01",
            "A_C_RABBIT_01",
            "A_C_RACCOON_01",
            "A_C_RAT_01",
            "A_C_RAVEN_01",
            "A_C_REDFOOTEDBOOBY_01",
            "A_C_ROOSTER_01",
            "A_C_ROSEATESPOONBILL_01",
            "A_C_SEAGULL_01",
            "A_C_SHARKHAMMERHEAD_01",
            "A_C_SHARKTIGER",
            "A_C_SHEEP_01",
            "A_C_SKUNK_01",
            "A_C_SNAKE_01",
            "A_C_SNAKEBLACKTAILRATTLE_01",
            "A_C_SNAKEFERDELANCE_01",
            "A_C_SNAKEREDBOA_01",
            "A_C_SNAKEWATER_01",
            "A_C_SQUIRREL_01",
            "A_C_TOAD_01",
            "A_C_TURKEY_01",
            "A_C_TURKEYWILD_01",
            "A_C_TURTLESEA_01",
            "A_C_VULTURE_01",
            "A_C_WOLF_MEDIUM",
            "A_C_WOLF_SMALL",
            "A_C_WOLF",
        };

        public static List<string> HorseHashes { get; }
            = new List<string>()
        {
            "A_C_HORSE_AMERICANPAINT_GREYOVERO",
            "A_C_HORSE_AMERICANPAINT_OVERO",
            "A_C_HORSE_AMERICANPAINT_SPLASHEDWHITE",
            "A_C_HORSE_AMERICANPAINT_TOBIANO",
            "A_C_HORSE_AMERICANSTANDARDBRED_BLACK",
            "A_C_HORSE_AMERICANSTANDARDBRED_BUCKSKIN",
            "A_C_HORSE_AMERICANSTANDARDBRED_PALOMINODAPPLE",
            "A_C_HORSE_AMERICANSTANDARDBRED_SILVERTAILBUCKSKIN",
            "A_C_HORSE_ANDALUSIAN_DARKBAY",
            "A_C_HORSE_ANDALUSIAN_ROSEGRAY",
            "A_C_HORSE_APPALOOSA_BLANKET",
            "A_C_HORSE_APPALOOSA_BROWNLEOPARD",
            "A_C_HORSE_APPALOOSA_LEOPARD",
            "A_C_HORSE_APPALOOSA_LEOPARDBLANKET",
            "A_C_HORSE_ARABIAN_BLACK",
            "A_C_HORSE_ARABIAN_ROSEGREYBAY",
            "A_C_HORSE_ARABIAN_WHITE",
            "A_C_HORSE_ARDENNES_BAYROAN",
            "A_C_HORSE_ARDENNES_STRAWBERRYROAN",
            "A_C_HORSE_BELGIAN_BLONDCHESTNUT",
            "A_C_HORSE_BELGIAN_MEALYCHESTNUT",
            "A_C_HORSE_DUTCHWARMBLOOD_CHOCOLATEROAN",
            "A_C_HORSE_DUTCHWARMBLOOD_SEALBROWN",
            "A_C_HORSE_DUTCHWARMBLOOD_SOOTYBUCKSKIN",
            "A_C_HORSE_GANG_BILL",
            "A_C_HORSE_GANG_CHARLES_ENDLESSSUMMER",
            "A_C_HORSE_GANG_CHARLES",
            "A_C_HORSE_GANG_DUTCH",
            "A_C_HORSE_GANG_HOSEA",
            "A_C_HORSE_GANG_JAVIER",
            "A_C_HORSE_GANG_JOHN",
            "A_C_HORSE_GANG_KAREN",
            "A_C_HORSE_GANG_KIERAN",
            "A_C_HORSE_GANG_LENNY",
            "A_C_HORSE_GANG_MICAH",
            "A_C_HORSE_GANG_SADIE_ENDLESSSUMMER",
            "A_C_HORSE_GANG_SADIE",
            "A_C_HORSE_GANG_SEAN",
            "A_C_HORSE_GANG_TRELAWNEY",
            "A_C_HORSE_GANG_UNCLE_ENDLESSSUMMER",
            "A_C_HORSE_GANG_UNCLE",
            "A_C_HORSE_HUNGARIANHALFBRED_DARKDAPPLEGREY",
            "A_C_HORSE_HUNGARIANHALFBRED_FLAXENCHESTNUT",
            "A_C_HORSE_HUNGARIANHALFBRED_PIEBALDTOBIANO",
            "A_C_HORSE_KENTUCKYSADDLE_BLACK",
            "A_C_HORSE_KENTUCKYSADDLE_CHESTNUTPINTO",
            "A_C_HORSE_KENTUCKYSADDLE_GREY",
            "A_C_HORSE_KENTUCKYSADDLE_SILVERBAY",
            "A_C_HORSE_MISSOURIFOXTROTTER_AMBERCHAMPAGNE",
            "A_C_HORSE_MISSOURIFOXTROTTER_SILVERDAPPLEPINTO",
            "A_C_HORSE_MORGAN_BAY",
            "A_C_HORSE_MORGAN_BAYROAN",
            "A_C_HORSE_MORGAN_FLAXENCHESTNUT",
            "A_C_HORSE_MORGAN_PALOMINO",
            "A_C_HORSE_MUSTANG_GRULLODUN",
            "A_C_HORSE_MUSTANG_TIGERSTRIPEDBAY",
            "A_C_HORSE_MUSTANG_WILDBAY",
            "A_C_HORSE_NOKOTA_BLUEROAN",
            "A_C_HORSE_NOKOTA_REVERSEDAPPLEROAN",
            "A_C_HORSE_NOKOTA_WHITEROAN",
            "A_C_HORSE_SHIRE_DARKBAY",
            "A_C_HORSE_SHIRE_LIGHTGREY",
            "A_C_HORSE_SUFFOLKPUNCH_REDCHESTNUT",
            "A_C_HORSE_SUFFOLKPUNCH_SORREL",
            "A_C_HORSE_TENNESSEEWALKER_BLACKRABICANO",
            "A_C_HORSE_TENNESSEEWALKER_CHESTNUT",
            "A_C_HORSE_TENNESSEEWALKER_DAPPLEBAY",
            "A_C_HORSE_TENNESSEEWALKER_FLAXENROAN",
            "A_C_HORSE_TENNESSEEWALKER_REDROAN",
            "A_C_HORSE_THOROUGHBRED_BLOODBAY",
            "A_C_HORSE_THOROUGHBRED_BRINDLE",
            "A_C_HORSE_THOROUGHBRED_DAPPLEGREY",
            "A_C_HORSE_TURKOMAN_DARKBAY",
            "A_C_HORSE_TURKOMAN_GOLD",
            "A_C_HORSE_TURKOMAN_SILVER",
            "A_C_HORSEMULE_01",
        };
    }
}
