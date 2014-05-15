(function () {
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
            title: '<span class="overskrift-shepherd">Velkommen utvikler :)</span>',
            text: ['<span class="sheperd-link"><a href="DefaultUtvikler.aspx?sheperd=false">Trykk her</a> for å deaktivere tutorialen ved framtidige besøk.<br />(Tutorialen kan reaktiveres under "innstillinger" i menyen)</span>',
            'Etter du har logget inn må du velge ett prosjekt før du går videre. ',
            'Det gjør du ved å velge ett prosjekt i listen nedenfor.',
            'Dersom du er faseleder for valgt prosjekt vil du se at hovedmenyen skifter farge fra svart til grønn, og du vil få noen ekstra valg i undermenyene.'],
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
            title: '<span class="overskrift-shepherd">Oversikt over valgt prosjekt.</span>',
            text: ['En oversikt over prosjektet du valgte finner du her!.',
            'Dersom du er faseleder for valgt prosjekt vil du i tillegg ha muligheten til å administrere faser her.'],
            attachTo: '.shepherd-prosjekt bottom',
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
            title: '<span class="overskrift-shepherd">Oversikt over team.</span>',
            text: ['Her ligger en oversikt over teamet i valgt prosjekt.'],
            attachTo: '.shepherd-team bottom',
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
            title: '<span class="overskrift-shepherd">Oversikt over oppgaver.</span>',
            text: ['Her ligger det en oversikt over alle dine oppgaver i valgt prosjekt og en oversikt over alle oppgaver i valgt prosjekt.',
            'Dersom du er faseleder for valgt prosjekt vil du i tillegg ha mulighet til å endre på oppgavene i valgt prosjekt.'],
            attachTo: '.shepherd-oppgaver bottom',
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
            title: '<span class="overskrift-shepherd">Timeregistrering.</span>',
            text: ['Her finner du en oversikt over alle dine registrerte timer, du kan registrere timer på oppgaver i valgt prosjekt og du kan endre/deaktivere dine registrerte timer.',
            'Dersom du er faseleder for valgt prosjekt vil du i tillegg ha mulighet til å godkjenne timer for andre utviklere som er registrert manuelt.'],
            attachTo: '.shepherd-timeregistrering bottom',
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
            title: '<span class="overskrift-shepherd">Rapporter</span>',
            text: ['På "vis rapporter" kan du få en rapport over team og prosjekt for valgt prosjekt, og du kan få en rapport over deg selv.',
            'Det er også muligheter for å eksportere rapportene til excel.',
            'Det er også mulig å få se og eksportere product backlog og sprint backlog her.'],
            attachTo: '.shepherd-rapporter bottom',
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
        shepherd.addStep('Konto', {
            title: '<span class="overskrift-shepherd">Innstillinger</span>',
            text: ['Her kan du endre på diverse innstillinger.',
            'Dette er slutten på omvisningen.',
            'Lykke til :)'],
            attachTo: '.shepherd-konto bottom',
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