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
            shepherd.addStep('RegistrerNyBruker', {
                title: '<span class="overskrift-shepherd">Velkommen administrator</span>',
                text: ['<span class="sheperd-link"><a href="DefaultAdministrator.aspx?sheperd=false">Trykk her</a> for å deaktivere tutorialen ved framtidige besøk.<br />(Tutorialen kan reaktivires under "innstillinger" i menyen)</span>',
                       'Her kan du registrere nye brukere', 'Du velger hvilken rettighet en bruker skal ha, skriver inn fornavn og etternavn og til slutt epost-adressen til vedkommede',
                       'Når du har trykket på "Registrer bruker" knappen, vil vedkommede få en epost med en aktiveringslink han/hun må trykke på for å fullføre registreringen. '
                      ],
                attachTo: '.adm1 bottom',
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
            shepherd.addStep('OversiktOverBrukere', {
                title: '<span class="overskrift-shepherd">Oversikt over brukere.</span>',
                text: ['En detaljert oversikt over brukere finner du her!.'],
                attachTo: '.adm3 bottom',
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
            
            shepherd.addStep('AdministrereRettigheter', {
                title: '<span class="overskrift-shepherd">Administrere rettigheter</span>',
                text: ['Her har du mulighet til å legge til rettigeter.',
                'Det vil også være valg for å endre navn på rettigheter som allerede er opprettet.'],
                attachTo: '.adm4 bottom',
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
            shepherd.addStep('EpostVarsler', {
                title: '<span class="overskrift-shepherd">Innstillinger</span>',
                text: ['Her kan du endre innstillinger, endre personlige opplysninger, bytte passord og endre innstillinger for epost varsling.',
                'Dette er slutten på omvisningen.',
                'Lykke til :)'],
                attachTo: '.adm5 bottom',
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