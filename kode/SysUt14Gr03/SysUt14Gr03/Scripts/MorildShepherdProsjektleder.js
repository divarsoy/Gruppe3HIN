﻿(function () {
    var completeShepherd, init, setupShepherd;

    init = function () {
        return setupShepherd();
    };

    setupShepherd = function () {
        var shepherd;
        shepherd = new Shepherd.Tour({
            defaults: {
                classes: 'shepherd-element shepherd-open shepherd-theme-arrows'
            }
        });
        shepherd.addStep('VelgProsjekt', {
            title: 'Velkommen prosjektleder :)',
            text: ['Etter du har logget inn må du velge ett prosjekt før du går videre.',
            'Det gjør du ved å velge ett prosjekt i listen nedenfor. '],
            attachTo: '.ShepherdVelgProsjekt bottom',
            classes: 'shepherd shepherd-open shepherd-theme-arrows shepherd-transparent-text',
            buttons: [
              {
                  text: 'Avslutt',
                  classes: 'shepherd-button-secondary',
                  action: function () {
                      completeShepherd();
                      return shepherd.hide();
                  }
              }, {
                  text: 'Neste',
                  action: shepherd.next,
                  classes: 'shepherd-button-example-primary'
              }
            ]
        });

        shepherd.addStep('Prosjekter', {
            title: 'Prosjekter!',
            text: ['Her finner du en oversikt over alle prosjekter, du kan opprette ett nytt prosjekt samt administrere alle prosjekter du er leder for og de prosjektene du oppretter.'],
            attachTo: '.ShepherdMenyProsjekter bottom',
            buttons: [
              {
                  text: 'Tilbake',
                  classes: 'shepherd-button-secondary',
                  action: shepherd.back
              }, {
                  text: 'Avslutt',
                  classes: 'shepherd-button-secondary',
                  action: function () {
                      completeShepherd();
                      return shepherd.hide();
                  }
              }, {
                  text: 'Neste',
                  action: shepherd.next
              }
            ]
        });

        shepherd.addStep('Team', {
            title: 'Team',
            text: ['Her finner du en oversikt over alle team og en oversikt over team i det prosjektet du valgte i listen i starten av touren.',
                'Du vil også ha et menyvalg der du kan administrere teamene. Der kan du legge til eller fjerne utviklere fra et team, og du kan arkivere ett team.',
                'Det siste menyvalget gir deg mulighet til å opprette ett team. Du må gi teamet ett navn og legge til ønskede utviklere.'],
            attachTo: '.ShepherdMenyTeam bottom',
            buttons: [
              {
                  text: 'Tilbake',
                  classes: 'shepherd-button-secondary',
                  action: shepherd.back
              }, {
                  text: 'Avslutt',
                  classes: 'shepherd-button-secondary',
                  action: function () {
                      completeShepherd();
                      return shepherd.hide();
                  }
              }, {
                  text: 'Neste',
                  action: shepherd.next
              }
            ]
        });

        shepherd.addStep('Brukere', {
            title: 'Brukere!',
            text: ['Her ligger det en oversikt over utviklere i prosjektet du valgte i starten, med diverse kontaktinformasjon ',
            'Du har også muligheten til å legge til en ny bruker.'],
            attachTo: '.ShepherdMenyBruker bottom',
            buttons: [
              {
                  text: 'Tilbake',
                  classes: 'shepherd-button-secondary',
                  action: shepherd.back
              }, {
                  text: 'Avslutt',
                  classes: 'shepherd-button-secondary',
                  action: function () {
                      completeShepherd();
                      return shepherd.hide();
                  }
              }, {
                  text: 'Neste',
                  action: shepherd.next
              }
            ]
        });

        shepherd.addStep('Oppgaver', {
            title: 'Oppgaver',
            text: ['Her ligger det en oversikt over oppgaver i valgt prosjekt, du kan opprette en ny oppgave i valg prosjekt, ',
            'samt opprette en oppgavegruppe i valgt prosjekt.'],
            attachTo: '.ShepherdMenyOppgaver bottom',
            buttons: [
              {
                  text: 'Tilbake',
                  classes: 'shepherd-button-secondary',
                  action: shepherd.back
              }, {
                  text: 'Avslutt',
                  classes: 'shepherd-button-secondary',
                  action: function () {
                      completeShepherd();
                      return shepherd.hide();
                  }
              }, {
                  text: 'Neste',
                  action: shepherd.next
              }
            ]
        });

        shepherd.addStep('Timeregistrering', {
            title: 'Timeregistrering',
            text: ['Her ligger det en oversikt registrerte timer.'],
            attachTo: '.ShepherdMenyTimereg bottom',
            buttons: [
              {
                  text: 'Tilbake',
                  classes: 'shepherd-button-secondary',
                  action: shepherd.back
              }, {
                  text: 'Avslutt',
                  classes: 'shepherd-button-secondary',
                  action: function () {
                      completeShepherd();
                      return shepherd.hide();
                  }
              }, {
                  text: 'Neste',
                  action: shepherd.next
              }
            ]
        });

        shepherd.addStep('Rapporter', {
            title: 'Rapporter',
            text: ['Her har du muligheten til å generere rapporter over team, prosjekt og individer.'],
            attachTo: '.ShepherdMenyRapporter bottom',
            buttons: [
              {
                  text: 'Tilbake',
                  classes: 'shepherd-button-secondary',
                  action: shepherd.back
              }, {
                  text: 'Avslutt',
                  classes: 'shepherd-button-secondary',
                  action: function () {
                      completeShepherd();
                      return shepherd.hide();
                  }
              }, {
                  text: 'Neste',
                  action: shepherd.next
              }
            ]
        });

        shepherd.addStep('Innstillinger', {
            title: 'Innstillinger',
            text: ['Her kan du endre innstillinger på hvilke epost varsler du vil motta.',
            'Dette er slutten på omvisningen.',
            'Lykke til :)'],
            attachTo: '.ShepherdMenyInnstillinger bottom',
            buttons: [
              {
                  text: 'Tilbake',
                  classes: 'shepherd-button-secondary',
                  action: shepherd.back
              }, {
                  text: 'Ferdig',
                  action: function () {
                      completeShepherd();
                      return shepherd.next();
                  }
              }
            ]
        });
        return shepherd.start();
    };

    completeShepherd = function () {
        return $('body').addClass('shepherd-completed');
    };

    $(init);

}).call(this);